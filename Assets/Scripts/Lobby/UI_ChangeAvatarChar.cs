using System;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;
using static LobbyManager;

public class UI_ChangeAvatarChar : MonoBehaviour
{
    public PlayerCharacter playerCharacter;
    private Button btnAvatar;
    private float lastUpdateTimer;
    private float UpdateCooldown =1.1f;

    private void Start() {
        btnAvatar = GetComponent<Button>();
        btnAvatar.onClick.AddListener(
            () =>
            {
                if(Time.time > lastUpdateTimer + UpdateCooldown)
                {
                    lastUpdateTimer = Time.time;
                    LobbyManager.Instance.UpdatePlayerCharacter(playerCharacter);
                }
            }
        );
    }

    
}
