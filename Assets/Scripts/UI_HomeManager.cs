using System.Collections.Generic;
using UnityEngine;

public class UI_HomeManager : ScreenManager
{
    [SerializeField]
    private GameObject m_Home;
    [SerializeField]
    private GameObject m_GameMode;


    private void Start()
    {
        m_Screens.Add("Home", m_Home);
        m_Screens.Add("GameMode", m_GameMode);
        NavigateTo("Home");
    }
}