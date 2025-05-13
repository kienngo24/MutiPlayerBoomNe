using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : Singleton<LobbyController> {
    [SerializeField] private Button btn_Ready;
    [SerializeField] private Button btn_Cancel;
    [SerializeField] private Button btn_Leave;
    private void Awake() {
        btn_Ready.onClick.AddListener(
            delegate 
            {
                Ready();
            }
        );
        btn_Cancel.onClick.AddListener(
            delegate 
            {
                
            }
        );
        btn_Leave.onClick.AddListener(
            delegate 
            {
                LeaveRoom();
            }
        );
    }
    private void Start() {
        LobbyManager.Instance.OnGameStart += LobbyManager_OnGameStart;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

    }
    private void OnClientDisconnected(ulong obj)
    {
        LobbyManager.Instance.LeavePlayer();
    }

    private void LobbyManager_OnGameStart(object sender, EventArgs e) => ScreenManager.Instance.HideUI();

    private void LeaveRoom()
    {
        LobbyManager.Instance.LeavePlayer();
    }
    private void Ready()
    {
        LobbyManager.Instance.StartGameAsync();
    }
}