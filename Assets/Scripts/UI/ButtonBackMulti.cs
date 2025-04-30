
using UnityEngine;

public class ButtonBackMulti : ActionButton
{
    [SerializeField]
    private string m_SceneName;


    public override void OnClick()
    {
        while (m_ScreenManager.GetStackSize() > 0 && m_ScreenManager.GetCurrentScreen() != m_SceneName)
        {
            m_ScreenManager.NavigateBack();
        }
    }
}