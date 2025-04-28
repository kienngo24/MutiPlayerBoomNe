using UnityEngine;

public class UILobbyTabOption : MonoBehaviour
{
    public GameObject subPage;
    public GameObject[] listSubPage;
    private Button_UI btn;
    private void Start() {

        btn = GetComponent<Button_UI>();
        btn.ClickFunc = () => 
        {
            TurnOf();
            subPage.SetActive(true);
        };
    }
    private void TurnOf()
    {
        foreach (var subPage in listSubPage)
            subPage.SetActive(false);
    }
}
