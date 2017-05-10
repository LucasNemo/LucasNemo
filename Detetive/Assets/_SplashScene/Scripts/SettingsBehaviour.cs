using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject m_main, m_credits, m_howToPlay, m_termConfirm, m_talkConfirm;

    public void OnBackMainClick()
    {
        FindObjectOfType<SplashController>().OnBackFromSettings();
    }
    
    public void OnCreditsClick()
    {
        m_main.SetActive(false);
        m_credits.SetActive(true);
    }

    public void OnBackFromCredits()
    {
        m_credits.SetActive(false);
        m_main.SetActive(true);
    }

    public void OnHowToPlayClick()
    {
        m_main.SetActive(false);
        m_howToPlay.SetActive(true);
    }

    public void OnBackFromHowToPlay()
    {
        m_main.SetActive(true);
        m_howToPlay.SetActive(false);
    }

    #region Term

    public void OnTermClick()
    {
        m_termConfirm.SetActive(true);
    }

    public void OnBackFromTermClick()
    {
        m_termConfirm.SetActive(false);
    }
    
    public void OnConfirmTerm()
    {
        Application.OpenURL(Manager.Instance.DETECTIVE_URL_TERM);
    }

    #endregion

    #region Talk

    public void OnTalkWithUsClick()
    {
        m_talkConfirm.SetActive(true);
    }
    
    public void OnBackFromOnTalkWithUsClick()
    {
        m_talkConfirm.SetActive(false);
    }

    public void OnConfirmOnTalkWithUs()
    {
        Application.OpenURL(Manager.Instance.DETECTIVE_URL_TALK);
    }

    #endregion

}
