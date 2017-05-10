using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject m_confirmContinue;

	public void OnChooseClick()
    {
        if (Manager.Instance.isOnGame)
        {
            m_confirmContinue.SetActive(true);
        }
        else
        {
            FindObjectOfType<SplashController>().OpenSelection();
        }
    }
    
    public void OnConfirmContinueClick()
    {
        SceneManager.LoadScene("DetectiveScene");
    }

    public void OnNewGameClick()
    {
        Manager.Instance.MyGameInformation = null;
        m_confirmContinue.SetActive(false);
        FindObjectOfType<SplashController>().OpenSelection();
    }

    public void OnSettingsClick()
    {
        FindObjectOfType<SplashController>().OpenSettings();
    }

}
