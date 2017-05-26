using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonBehaviour : MonoBehaviour, IBackButton {

    [SerializeField]
    private bool m_backToGameObject;

    [SerializeField]
    private List<GameObject> m_objectsToActivate;

    [SerializeField]
    private string m_sceneNameToBack;   

    public void OnAndroidBackButtonClick()
    {
        if(m_backToGameObject)
        {
            m_objectsToActivate.ForEach(x => x.SetActive(true));
            gameObject.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(m_sceneNameToBack);
        }
    }

    private void OnEnable()
    {
            BackButtonManager.Instance.AddBackButton(this);
    }

    private void OnDisable()
    {
            BackButtonManager.Instance.RemoveBackButton(this);
    }

}
