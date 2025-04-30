using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListUI : MonoBehaviour {


    public static LobbyListUI Instance { get; private set; }


    [SerializeField] private Transform parent;
    [SerializeField] private Transform lobbySingleTemplate;
    [SerializeField] private Transform container;
    [SerializeField] private Button refreshButton;
    [SerializeField] private Button backRoom;
    [SerializeField] private Button_UI joincode;  
    private string code ="";

    private void Awake() {
        Instance = this;

        lobbySingleTemplate.gameObject.SetActive(false);

        refreshButton.onClick.AddListener(RefreshButtonClick);
        //createLobbyButton.onClick.AddListener(CreateLobbyButtonClick);
        joincode.ClickFunc = () => 
        {
            UI_InputWindow.Show_Static("Player Name", code, "abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ123456789", 10,
            () => {
                // Cancel
            },
            (string newName) => {
                code = newName;
                LobbyManager.Instance.JoinLobbyByCode(code);
            });
        };
    }

    private void Start() {
        LobbyManager.Instance.OnLobbyListChanged += LobbyManager_OnLobbyListChanged;
        LobbyManager.Instance.OnJoinedLobby += LobbyManager_OnJoinedLobby;
        LobbyManager.Instance.OnLeftLobby += LobbyManager_OnLeftLobby;
        LobbyManager.Instance.OnKickedFromLobby += LobbyManager_OnKickedFromLobby;

    }
    
    private void LobbyManager_OnKickedFromLobby(object sender, LobbyManager.LobbyEventArgs e) {
        ScreenManager.Instance.NavigateBack();
        backRoom.gameObject.SetActive(false);
    }

    private void LobbyManager_OnLeftLobby(object sender, EventArgs e) {
        ScreenManager.Instance.NavigateBack();
        backRoom.gameObject.SetActive(false);
    }

    private void LobbyManager_OnJoinedLobby(object sender, LobbyManager.LobbyEventArgs e) {
        Debug.Log("CharacterSelect");
        ScreenManager.Instance.NavigateTo("CharacterSelect");
        backRoom.gameObject.SetActive(true);
    }

    private void LobbyManager_OnLobbyListChanged(object sender, LobbyManager.OnLobbyListChangedEventArgs e) {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList) {
        foreach (Transform child in container) {
            if (child == lobbySingleTemplate) continue;

            Destroy(child.gameObject);
        }

        foreach (Lobby lobby in lobbyList) {
            Transform lobbySingleTransform = Instantiate(lobbySingleTemplate, container);
            lobbySingleTransform.gameObject.SetActive(true);
            LobbyListSingleUI lobbyListSingleUI = lobbySingleTransform.GetComponent<LobbyListSingleUI>();
            lobbyListSingleUI.UpdateLobby(lobby);
        }
    }

    private void RefreshButtonClick() {
        LobbyManager.Instance.RefreshLobbyList();
    }

    public void CreateLobbyButtonClick() {
        parent.gameObject.SetActive(false);  
    }

}