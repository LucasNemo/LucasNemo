using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour {

    [SerializeField]
    private GameObject m_intro, m_menu, m_selection;

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
    
    public void OnBackFromSelection()
    {
        m_selection.SetActive(false);
        m_menu.SetActive(true);
    }

}
