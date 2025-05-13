using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyAccessBlocker : MonoBehaviour
{
    [SerializeField] private GameObject ui_LobbyBlocker;
    private void Start() {
        UnLobbyBlocker();

        LobbyManager.Instance.OnJoinedLobby += OnLobbyJoined;
        LobbyManager.Instance.OnKickedFromLobby += OnLobbyJoined;
        LobbyManager.Instance.OnRemoveLobby += OnLobbyJoined;
        LobbyManager.Instance.OnLeftLobby += OnLobbyJoined;

    }

    private void OnLobbyJoined(object sender, EventArgs e)
    {
        UnLobbyBlocker();
    }

    private void OnLobbyJoined(object sender, LobbyManager.LobbyEventArgs e)
    {
        LobbyBlocker();
    }

    private void LobbyBlocker() => ui_LobbyBlocker.SetActive(true);
    private void UnLobbyBlocker() => ui_LobbyBlocker.SetActive(false);

}
