﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultBehaviour : MonoBehaviour
{

    [SerializeField]
    private Text m_title, m_description;

    public void UpdateInformation(bool win)
    {
        DateTime oldDate;

        DateTime.TryParse(PlayerPrefs.GetString(Manager.Instance.PLAYER_SAVE_TIME), out oldDate);

        TimeSpan span = new TimeSpan();
        if (oldDate != null)
            span = (DateTime.Now - oldDate);

        m_title.text = win ? Manager.Instance.WIN_TITLE : Manager.Instance.LOOSE_TITLE;
        m_description.text = win ? string.Format(Manager.Instance.WIN_DESCRIPTION, span.Hours, span.Minutes) : Manager.Instance.LOOSE_DESCRIPTION;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(Manager.Instance.MENU_SCENE);
    }

}
