﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionBehaviour : MonoBehaviour, IBackButton
{
    public void OnBackClick()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        FindObjectOfType<SplashController>().OnBackFromSelection();
    }
    
    public void OpenSettings()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        FindObjectOfType<SplashController>().OpenSettings();
    }

    public void OnSheriffSelect()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        SceneManager.LoadScene(Manager.Instance.SHERIFFE_SCENE);
    }

    public void OnDetectiveSelect()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        SceneManager.LoadScene(Manager.Instance.DETETIVE_SCENE);
    }
    
    public void OnAndroidBackButtonClick()
    {
        OnBackClick();
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
