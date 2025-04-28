using System;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterReady : Singleton<UICharacterReady>
{
    [SerializeField] private Button outRoom;



    private void Start() {
        outRoom.onClick.AddListener(() =>
        {
            LobbyManager.Instance.KickPlayer(AuthenticationService.Instance.PlayerId);

            Hide();
        });
        LobbyManager.Instance.OnJoinedLobby += LobbyManager_OnLobbyListChanged;
        Hide();
    }

    private void LobbyManager_OnLobbyListChanged(object sender, LobbyManager.LobbyEventArgs e)
    {
        Show();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
    
}
