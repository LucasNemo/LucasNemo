using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_visit;

    private bool isOnHome = true;

    public void OnVisitClick()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        // m_visit.SetActive(false);
        GenericModal.Instance.OpenModal(Manager.Instance.CONFIRM_VISIT_MODAL_TITLE, "Voltar", "Continuar", () =>
        {
            //Dummy
        },
        () =>
        {
            OnGoToSite();
        }, false);
    }

    private void OnGoToSite()
    {
        Application.OpenURL(Manager.Instance.DETECTIVE_URL);
    }

    public void OnContinueClick()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
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
        SceneManager.LoadScene(Manager.Instance.DETETIVE_SCENE);
    }

    private void OnNewGameClick()
    {
        Manager.Instance.ClearData();
        FindObjectOfType<SplashController>().OpenSelection();
    }

    private void BackFromIntro()
    {
        GenericModal.Instance.OpenModal(Manager.Instance.REALLY_WANNA_EXIT, "Não", "Sim", () =>
        {
            Application.Quit();
        },
        () =>
        {
            //dummy
        });
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackFromIntro();
    }
}
