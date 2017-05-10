using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBehaviour : MonoBehaviour {
    
    [SerializeField]
    private GameObject m_visit, m_visitConfirm;
    
    public void OnVisitClick()
    {
        m_visit.SetActive(false);
        m_visitConfirm.SetActive(true);
    }

    public void OnCancelConfirmClick()
    {
        m_visit.SetActive(true);
        m_visitConfirm.SetActive(false);
    }

    public void OnGoToSite()
    {
        Application.OpenURL(Manager.Instance.DETECTIVE_URL);
    }

    public void OnContinueClick()
    {
        FindObjectOfType<SplashController>().OpenMenu();
    }
}
