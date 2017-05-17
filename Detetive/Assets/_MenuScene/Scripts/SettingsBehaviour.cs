using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject m_main, m_credits, m_howToPlay;

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
    
    public void OnTermClick()
    {
        GenericModal.Instance.OpenModal(Manager.Instance.TERMS_MODAL_DESCRIPTION, "Voltar", "Continuar", null,
       () =>
       {
            OnConfirmTerm();
       });
    }
      
    public void OnConfirmTerm()
    {
        Application.OpenURL(Manager.Instance.DETECTIVE_URL_TERM);
    }
    
    public void OnTalkWithUsClick()
    {
        GenericModal.Instance.OpenModal(Manager.Instance.TALK_MODAL_DESCRIPTION, "Voltar", "Continuar",null,
        () =>
        {
            OnConfirmOnTalkWithUs();
        });
    }
    
    public void OnConfirmOnTalkWithUs()
    {
        Application.OpenURL(Manager.Instance.DETECTIVE_URL_TALK);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_howToPlay.activeSelf) OnBackFromHowToPlay();
            else if (m_credits.activeSelf) OnBackFromCredits();
            else if (m_main.activeSelf) OnBackMainClick();
        }
    }

}
