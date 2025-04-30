using System;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;
using static LobbyManager;

public class UI_ChangeAvatarChar : MonoBehaviour
{
    public PlayerCharacter playerCharacter;
    private Button btnAvatar;
    private void Start() {
        btnAvatar = GetComponent<Button>();
        btnAvatar.onClick.AddListener(
            () =>
            {
                LobbyManager.Instance.UpdatePlayerCharacter(playerCharacter);
            }
        );
    }

    
}
