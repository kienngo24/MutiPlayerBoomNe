using System.Linq;
using TMPro;
using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    private Button_UI PopupName;
    private TextMeshProUGUI userName;
    private string playerName = "Kien";
    private void Awake() {
        PopupName = GetComponentsInChildren<Button_UI>().FirstOrDefault(btn => btn.gameObject.name == "BtnName");
        userName = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(btn => btn.gameObject.name == "txtname");
    }
    private void Start() {
        PopupName.ClickFunc = () => 
        {
            
            UI_InputWindow.Show_Static("Player Name", playerName, "abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ .,-", 20,
            () => {
                // Cancel
            },
            (string newName) => {
                userName.text = newName;
                playerName = newName;
            });
        };
    }
    private void Update() {
        
    }
    


}
