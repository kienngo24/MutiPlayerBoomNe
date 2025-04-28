using System.Collections.Generic;
using UnityEngine;

public struct NavigationData
{
    public string ScreenName;
    public object Data;
}

public abstract class ScreenManager : MonoBehaviour
{

    protected Stack<NavigationData> m_NavigationStack = new();
    protected Dictionary<string, GameObject> m_Screens = new();
    protected GameObject m_CurrentScreen;



    public virtual void NavigateTo(string screenName, object data = null)
    {
        m_NavigationStack.Push(new NavigationData { ScreenName = screenName, Data = data });
        OnStackChanged();
    }

    public virtual void NavigateBack()
    {
        if (m_NavigationStack.Count > 0)
        {
            m_NavigationStack.Pop();
            OnStackChanged();
        }
    }

    public void OnStackChanged()
    {
        if (m_NavigationStack.Count > 0)
        {
            var screenName = m_NavigationStack.Peek().ScreenName;

            if (m_Screens.ContainsKey(screenName))
            {
                if (m_CurrentScreen != null)
                {
                    m_CurrentScreen.SetActive(false);
                }

                m_CurrentScreen = m_Screens[screenName];
                m_CurrentScreen.SetActive(true);
            }
        }
    }

    public object GetNavigationData()
    {
        if (m_NavigationStack.Count > 0)
        {
            return m_NavigationStack.Peek().Data;
        }

        return null;
    }
}