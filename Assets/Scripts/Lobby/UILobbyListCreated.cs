using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static LobbyManager;

public class UILobbyListCreated : MonoBehaviour
{

    [SerializeField] private GameMode gameMode;
    [SerializeField] private Button_UI btnMaxPlayer;
    [SerializeField] private Button_UI btnIsPrivate;
    [SerializeField] private Button_UI btnCreate;
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI txtGameMode;
    [SerializeField] private TextMeshProUGUI txtMaxPlayer;
    private bool isPrivate;
    private int maxPlayers = 1;
    private void Awake() {
        btnCreate = GetComponentsInChildren<Button_UI>().FirstOrDefault(btn => btn.gameObject.name == "btnCreate");
        btnIsPrivate = GetComponentsInChildren<Button_UI>().FirstOrDefault(btn => btn.gameObject.name == "btnIsPrivate");
        btnMaxPlayer = GetComponentsInChildren<Button_UI>().FirstOrDefault(btn => btn.gameObject.name == "btnMaxPlayer");
        avatar = transform.Find("Icon").GetComponent<Image>();

        txtMaxPlayer = transform.Find("MaxPlayer/txtMaxPlayer").GetComponent<TextMeshProUGUI>();
        txtGameMode = transform.Find("txtGameMode").GetComponent<TextMeshProUGUI>();
        
    }
    
    private void Start() {
        btnCreate.SetHoverBehaviourType();
        btnMaxPlayer.SetHoverBehaviourType();
        btnIsPrivate.SetToggleBehaviour();
        
        isPrivate = btnIsPrivate.GetToggle();
        btnCreate.ClickFunc = () =>
        {
            Debug.Log("Play : "+ isPrivate.ToString() + " " + gameMode.ToString() + " " +maxPlayers.ToString());
            LobbyManager.Instance.CreateLobby("Lobby Room",maxPlayers,isPrivate, gameMode,avatar.sprite);
        };
        btnIsPrivate.ClickFunc = () =>
        {
            isPrivate = btnIsPrivate.GetToggle();
            Debug.Log(isPrivate);
        };
        btnMaxPlayer.ClickFunc = () =>
        {
            UI_InputWindow.Show_Static("Player Name", maxPlayers.ToString(), "123456789", 1,
            () => {
                // Cancel
            },
            (string newName) => {
                txtMaxPlayer.text = "max player " + newName;
                maxPlayers = int.Parse(newName);
            });
        };
    }
    public void SetUpData(Room room)
    {
        txtGameMode.text = room.gameMode.ToString();
        txtMaxPlayer.text = "max player "  + room.maxPlayers.ToString();
        maxPlayers = room.maxPlayers;
        isPrivate = room.isPrivate;
        avatar.sprite = room.avatar;

    }
}
