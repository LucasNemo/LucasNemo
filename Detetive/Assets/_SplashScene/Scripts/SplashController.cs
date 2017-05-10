using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour {

    [SerializeField]
    private GameObject m_intro, m_menu, m_selection, m_settings;

	public void OpenMenu()
    {
        m_intro.SetActive(false);
        m_menu.gameObject.SetActive(true);
    }
    
    public void OpenSelection()
    {
        m_menu.SetActive(false);
        m_selection.SetActive(true);
    }

    public void OpenSettings()
    {
        m_menu.SetActive(false);
        m_settings.SetActive(true);
    }
    
    public void OnBackFromSelection()
    {
        m_selection.SetActive(false);
        m_menu.SetActive(true);
    }

    public void OnBackFromSettings()
    {
        m_settings.SetActive(false);
        m_menu.SetActive(true);
    }

}
