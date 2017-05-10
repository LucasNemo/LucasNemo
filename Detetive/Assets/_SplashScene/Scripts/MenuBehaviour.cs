using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject m_confirmContinue;

    public void OnChooseClick()
    {
        if (Manager.Instance.isOnGame)
        {
            GenericModal.Instance.OpenModal(Manager.Instance.CONTINUE_GAME_MODAL_DESCRIPTION, "Novo jogo", "Continuar", () =>
            {
                GenericModal.Instance.CloseModal();
                OnNewGameClick();
            },
            () =>
            {
                OnConfirmContinueClick();
            });
        }
        else
        {
            FindObjectOfType<SplashController>().OpenSelection();
        }
    }

    private void OnConfirmContinueClick()
    {
        SceneManager.LoadScene("DetectiveScene");
    }

    private void OnNewGameClick()
    {
        Manager.Instance.MyGameInformation = null;
        FindObjectOfType<SplashController>().OpenSelection();
    }

    public void OnSettingsClick()
    {
        FindObjectOfType<SplashController>().OpenSettings();
    }

}
