using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{

      
    IAuthentication authentication;
    public const string KEY_PLAYER_NAME = "PlayerName";
    public const string KEY_PLAYER_CHARACTER = "Character";
    public const string KEY_GAME_MODE = "GameMode";
    public event EventHandler OnLeftLobby;
    public event EventHandler OnRemoveLobby;
    public event EventHandler OnGameStart;


    public event EventHandler<LobbyEventArgs> OnJoinedLobby;
    public event EventHandler<LobbyEventArgs> OnJoinedLobbyUpdate;
    public event EventHandler<LobbyEventArgs> OnKickedFromLobby;
    public event EventHandler<LobbyEventArgs> OnLobbyGameModeChanged;
    
    public class LobbyEventArgs : EventArgs {
        public Lobby lobby;
    }
    public event EventHandler<OnLobbyListChangedEventArgs> OnLobbyListChanged;
    public class OnLobbyListChangedEventArgs : EventArgs {
        public List<Lobby> lobbyList;
    }


    public enum GameMode {
        CaptureTheFlag,
        Conquest,
        Survive,
        PickUpCoins
    }

    public enum PlayerCharacter {
        Marine,
        Ninja,
        Zombie
    }
    private float heartbeatCooldown = 30;
    private float lastHeartbeatTimer;
    private float lobbyPollTimer;
    private float lastRefreshTimer;
    private float RefreshCooldown = 10f;
    private float lastPoolTimer;
    private float poolCooldown = 5f;
    private Lobby joinedLobby;
    private string playerName;

    private void Start() {
        authentication = new Anonymous();
        authentication.AuthenticationAsync();
    }
    private void Update() {
        HandleLobbyHeartbeat();
        HandleRefreshLobbyList();
        HandleLobbyPolling();
    }
    private async void HandleLobbyHeartbeat() {
        if (IsLobbyHost()) {
            if (Time.time > lastHeartbeatTimer + heartbeatCooldown) {
                lastHeartbeatTimer = Time.time;
                await LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);
            }
        }
    }
    private void HandleRefreshLobbyList() {
        if (UnityServices.State == ServicesInitializationState.Initialized && AuthenticationService.Instance.IsSignedIn) {
            // if (Time.time > lastRefreshTimer + RefreshCooldown) {
            //     lastRefreshTimer = Time.time;
            //     RefreshLobbyList();
            // }
            //ManagerTimeLobby.Instance.RequestRefreshLobbyServerRpc();
        }
    }
    private async void HandleLobbyPolling() {
        if (joinedLobby != null) {
            
            if (Time.time  > lastPoolTimer + poolCooldown) {
                lastPoolTimer = Time.time;
                joinedLobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

                if (!IsPlayerInLobby()) {
                    // Player was kicked out of this lobby
                    Debug.Log("Kicked from Lobby!");

                    OnKickedFromLobby?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

                    joinedLobby = null;
                }
            }
        }
    }
    private bool IsPlayerInLobby() {
        try
        {
            if (joinedLobby != null && joinedLobby.Players != null) {
                foreach (Player player in joinedLobby.Players) {
                    if (player.Id == AuthenticationService.Instance.PlayerId) {
                        // This player is in this lobby
                        return true;
                    }
                }
            }
            return false;

        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
        return false;
    }
    public bool IsLobbyHost() {
        try
        {
            return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
        return false;
    }
    private Player GetPlayer() {
        try
        {
            return new Player(AuthenticationService.Instance.PlayerId, null, new Dictionary<string, PlayerDataObject> {
                { KEY_PLAYER_NAME, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerName) },
                { KEY_PLAYER_CHARACTER, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, PlayerCharacter.Marine.ToString()) }
            });

        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
        return null;
    }
    public async void CreateLobby(string lobbyName, int maxPlayers, bool isPrivate, GameMode gameMode, Sprite sprite)
    {
        //convert form sprite to  string
        //string base64String = SpriteHelper.SpriteToBase64(sprite); 
        try
        {
            Player player = GetPlayer();

            CreateLobbyOptions options = new CreateLobbyOptions {
                Player = player,
                IsPrivate = isPrivate,
                IsLocked = false,
                Data = new Dictionary<string, DataObject> {
                    { KEY_GAME_MODE, new DataObject(DataObject.VisibilityOptions.Public, gameMode.ToString()) },
                    { "Max Player", new DataObject(DataObject.VisibilityOptions.Public, maxPlayers.ToString()) },
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);

            joinedLobby = lobby;
            Debug.Log(joinedLobby.LobbyCode);

            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    public async void JoinLobbyByCode(string lobbyCode) {
        try
        {
            Player player = GetPlayer();

            Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, new JoinLobbyByCodeOptions {
                Player = player
            });

            joinedLobby = lobby;
            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
        } catch (LobbyServiceException e) {
            Debug.Log(e);
            Debug.Log("❌ Không tìm thấy lobby với mã đã nhập.");
        }
    }
    public async void JoinLobby(Lobby lobby) {
        try
        {
            Player player = GetPlayer();

            joinedLobby = await LobbyService.Instance.JoinLobbyByIdAsync(lobby.Id, new JoinLobbyByIdOptions {
                Player = player
            });

            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
            
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
    public async void QuickJoinLobby() {
        try {
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();

            Lobby lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
            joinedLobby = lobby;

            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
    public async void RefreshLobbyList() {
        try {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;

            // Filter for open lobbies only 
            options.Filters = new List<QueryFilter> {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value: "0")
            };

            // Order by newest lobbies first
            options.Order = new List<QueryOrder> {
                new QueryOrder(
                    asc: false,
                    field: QueryOrder.FieldOptions.Created)
            };

            QueryResponse lobbyListQueryResponse = await LobbyService.Instance.QueryLobbiesAsync();
            Debug.Log(lobbyListQueryResponse.Results.Count);
            OnLobbyListChanged?.Invoke(this, new OnLobbyListChangedEventArgs { lobbyList = lobbyListQueryResponse.Results });
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
    public async void UpdatePlayerCharacter(PlayerCharacter playerCharacter)
    {
        if(joinedLobby != null)
        {
            try
            {
                UpdatePlayerOptions options = new UpdatePlayerOptions();
                options.Data = new Dictionary<string, PlayerDataObject>{ 
                    { KEY_PLAYER_CHARACTER, new PlayerDataObject(visibility : PlayerDataObject.VisibilityOptions.Public,value: playerCharacter.ToString()) },
                };
                string playerId = AuthenticationService.Instance.PlayerId;
                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id,playerId,options);
                joinedLobby = lobby;
                OnJoinedLobbyUpdate?.Invoke(this,new LobbyEventArgs{lobby = joinedLobby});
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }
    private Player GetPlayerOpption()
    {
        try
        {
            return new Player
            {
                Data = new Dictionary<string, PlayerDataObject>
                {
                    { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName)},
                }
            };
            
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
        return null;
    }
    public Lobby GetJoinedLobby() => joinedLobby;
    public bool IsHasLobby() => joinedLobby != null;
    
    public async void KickPlayer(string playerId) {
        try
        {
            if (IsLobbyHost()) {
                try {
                    await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
                } catch (LobbyServiceException e) {
                    Debug.Log(e);
                }
            }
            
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
    public async void RemoveLobbyAsync()
    {
        try
        {
            await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);
            OnRemoveLobby?.Invoke(this,null);
        
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void StartGameAsync()
    {
        try {
            if(joinedLobby.HostId == AuthenticationService.Instance.PlayerId) 
            {
                NetworkManager.Singleton.StartHost();
                OnGameStart?.Invoke(this,null);
                await LobbyService.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject>
                    {
                        { "gameStarted", new DataObject(DataObject.VisibilityOptions.Member, "true") }
                    }
                });
            }
            else
            {
                await WaitForGameStartedFlag();
                NetworkManager.Singleton.StartClient();
                OnGameStart?.Invoke(this,null);
            }
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
    private async Task WaitForGameStartedFlag()
    {
        bool gameStarted = false;
        while (!gameStarted)
        {
            var lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
            if (lobby.Data.ContainsKey("gameStarted") && lobby.Data["gameStarted"].Value == "true")
            {
                gameStarted = true;
                Debug.Log("Host has started the game.");
            }
            else
            {
                await Task.Delay(1000); // đợi 1 giây trước khi kiểm tra lại
            }
        }
    }
    public async void LeavePlayer()
    {
        try {
            string playerId = AuthenticationService.Instance.PlayerId;
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
            joinedLobby = null;
            OnLeftLobby?.Invoke(this, null);
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
}
