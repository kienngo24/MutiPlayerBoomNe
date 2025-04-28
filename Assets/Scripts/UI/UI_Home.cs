using UnityEngine;
using UnityEngine.UI;

public class UI_Home : Singleton<UI_Home>
{
    [SerializeField] private Button play;
    private void Start() {
        play.onClick.AddListener(
            () => { 
                Debug.Log("Play game");
            }
        );
    }
    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
