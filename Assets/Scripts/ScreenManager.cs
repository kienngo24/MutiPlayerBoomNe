using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public struct NavigationData
{
    public string ScreenName;
    public object Data;
}

public class ScreenManager : Singleton<ScreenManager>
{
    protected Stack<NavigationData> m_NavigationStack = new();
    protected Dictionary<string, GameObject> m_Screens = new();
    protected GameObject m_CurrentScreen;



    [SerializeField]
    private GameObject m_GameMenuScreen;
    [SerializeField]
    private GameObject m_HomeScreen;
    [SerializeField]
    private GameObject m_AboutScreen;
    [SerializeField]
    private GameObject m_MenuScreen;
    [SerializeField]
    private GameObject m_GameModde;
    [SerializeField]
    private GameObject m_CharacterSelect;


    
    private void Awake()
    {
        m_Screens.Add("GameMenu", m_GameMenuScreen);
        m_Screens.Add("Home", m_HomeScreen);
        m_Screens.Add("About", m_AboutScreen);
        m_Screens.Add("Menu", m_MenuScreen);
        m_Screens.Add("GameMode", m_GameModde);
        m_Screens.Add("CharacterSelect", m_CharacterSelect);
    }
    private void Start() {
        foreach (var ui in m_Screens)
            Hide(ui.Value);
        NavigateTo("GameMenu");
    }
    private void Hide(GameObject gameObject) => gameObject.SetActive(false);

    public void NavigateTo(string screenName, object data = null)
    {
        m_NavigationStack.Push(new NavigationData { ScreenName = screenName, Data = data });
        OnStackChanged();
    }

    public void NavigateBack()
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
    public int GetStackSize() => m_NavigationStack.Count; 
    public string GetCurrentScreen() => m_CurrentScreen.gameObject.name;
}