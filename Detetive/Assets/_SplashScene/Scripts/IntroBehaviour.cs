using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        },
        () =>
        {
            OnGoToSite();
        });
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
        FindObjectOfType<SplashController>().OpenMenu();
    }
}
