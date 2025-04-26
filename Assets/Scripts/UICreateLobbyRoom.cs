using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LobbyManager;

public class UICreateLobbyRoom : Singleton<UICreateLobbyRoom>
{
    public ListLobbyCreate[] listLobbyCreates;
    private void Awake() {
        listLobbyCreates = GetComponentsInChildren<ListLobbyCreate>();
    }

}
