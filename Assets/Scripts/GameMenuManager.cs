using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : ScreenManager
{
    [SerializeField]
    private GameObject m_GameMenuScreen;
    [SerializeField]
    private GameObject m_HomeScreen;
    [SerializeField]
    private GameObject m_AboutScreen;
    [SerializeField]
    private GameObject m_MenuScreen;

    private void Start()
    {
        m_Screens.Add("GameMenu", m_GameMenuScreen);
        m_Screens.Add("Home", m_HomeScreen);
        m_Screens.Add("About", m_AboutScreen);
        m_Screens.Add("Menu", m_MenuScreen);
        NavigateTo("GameMenu");
    }
}