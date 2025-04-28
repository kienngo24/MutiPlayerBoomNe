using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LobbyManager;

public class UILobby : Singleton<UILobby>
{
    public UILobbyListCreated[] listLobbyCreates;
    private void Awake() {
        listLobbyCreates = GetComponentsInChildren<UILobbyListCreated>();
        LobbyManager.Instance.OnJoinedLobby += LobbyManager_OnJoinedLobby;
    }

    private void LobbyManager_OnJoinedLobby(object sender, LobbyEventArgs e)
    {
        Hide();
    }
    public void ShowLobbyManager_UI()
    {
        Show();
    }
    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
