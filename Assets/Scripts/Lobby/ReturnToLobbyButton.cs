using UnityEngine;
using UnityEngine.UI;

public class ReturnToLobbyButton : MonoBehaviour
{
    private Button btnToReturn;
    
    private bool canChange = true;
    private void Start() {
        btnToReturn = GetComponentInChildren<Button>();
        btnToReturn.onClick.AddListener(
            delegate 
            {
                ScreenManager.Instance.NavigateTo("CharacterSelect");
            }
        );
    }
    private void Update() {
        OnReturnButtonClicked();
    }

    public void OnReturnButtonClicked()
    {
        if(canChange == IsStillInLobby())
            return;
        canChange = IsStillInLobby();
        if (canChange)
            Show();
        else
            Hide();
    }

    private bool IsStillInLobby()
    {
        return LobbyManager.Instance != null && LobbyManager.Instance.IsHasLobby();
    }
    private void Show() => btnToReturn.gameObject.SetActive(true);
    private void Hide() => btnToReturn.gameObject.SetActive(false);
}
