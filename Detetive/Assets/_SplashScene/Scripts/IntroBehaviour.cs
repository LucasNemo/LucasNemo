using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_visit;

    public void OnVisitClick()
    {
        m_visit.SetActive(false);
        GenericModal.Instance.OpenModal(Manager.Instance.CONFIRM_VISIT_MODAL_TITLE, "Voltar", "Continuar", () =>
        {
            OnCancelConfirmClick();
            GenericModal.Instance.CloseModal(true);
        },
        () =>
        {
            OnGoToSite();
        }, false);
    }

    private void OnCancelConfirmClick()
    {
        m_visit.SetActive(true);
    }

    private void OnGoToSite()
    {
        Application.OpenURL(Manager.Instance.DETECTIVE_URL);
    }

    public void OnContinueClick()
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

    private void OnConfirmContinueClick()
    {
        SceneManager.LoadScene("DetectiveScene");
    }

    private void OnNewGameClick()
    {
        Manager.Instance.MyGameInformation = null;
        FindObjectOfType<SplashController>().OpenSelection();
    }
}
