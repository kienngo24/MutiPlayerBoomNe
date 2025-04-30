
using UnityEngine;
using UnityEngine.UI;

public abstract class ActionButton : MonoBehaviour
{
    private Button m_Button;
    protected ScreenManager m_ScreenManager;

    private void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClick);
        m_ScreenManager = FindAnyObjectByType<ScreenManager>(); 
    }
    public abstract void OnClick();
}