using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI player;
    [SerializeField] private TextMeshProUGUI gameModeText;
    private Lobby lobby;

    private float lastJoinTime = 0f;
    private float joinCooldown = 1.1f;
    private void Awake() {
        GetComponentInChildren<Button>().onClick.AddListener(() => {
            if(Time.time > lastJoinTime + joinCooldown)
            {
                lastJoinTime = Time.time;
                LobbyManager.Instance.JoinLobby(lobby);
            }
        });  
    }
    public void UpdateLobby(Lobby lobby) {
        this.lobby = lobby;
        player.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
        gameModeText.text = lobby.Data[LobbyManager.KEY_GAME_MODE].Value;
    }

}
