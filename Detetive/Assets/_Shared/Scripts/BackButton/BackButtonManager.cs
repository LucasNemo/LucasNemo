using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackButtonManager : SingletonBehaviour<BackButtonManager>
{
    private List<IBackButton> m_listBacks = new List<IBackButton>();

    public void AddController(IBackButton controller)
    {
        m_listBacks.Add(controller);
    }

    public void RemoveBackButton(IBackButton controller)
    {
        if (m_listBacks != null)
            m_listBacks.Remove(controller);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            HandleBackButton();
    }

    void HandleBackButton()
    {
        if (m_listBacks.Count > 0)
        {
            m_listBacks.LastOrDefault().OnAndroidBackButtonClick();
        }
        else
        {
            Application.Quit();
        }
    }
}

