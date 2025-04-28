
using UnityEngine;
using UnityEngine.UI;

public abstract class ActionButton : MonoBehaviour
{
    private Button m_Button;
    [SerializeField] protected ScreenManager m_ScreenManager;

    private void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClick);
    }
    public abstract void OnClick();
    [ContextMenu("Find Screen Manager")]
    public void FindScreenManager()
    {
        Transform current = transform;
        while (current != null)
        {
            m_ScreenManager = current.GetComponent<ScreenManager>();
            if(m_ScreenManager != null)
                break;
            current = current.parent;
        }

        if(m_ScreenManager == null)
            Debug.Log("Not Found");
    }
}