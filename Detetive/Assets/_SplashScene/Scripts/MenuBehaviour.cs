﻿using System.Collections;
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

    public void CreateGameClick()
    {
        SceneManager.LoadScene("SheriffScene");
    }

    private void OnConfirmContinueClick()
    {
        SceneManager.LoadScene("DetectiveScene");
    }

    private void OnNewGameClick()
    {
        Manager.Instance.MyGameInformation = null;
        SceneManager.LoadScene("DetectiveScene");
    }

    public void OnSettingsClick()
    {
        FindObjectOfType<SplashController>().OpenSettings();
    }

}
