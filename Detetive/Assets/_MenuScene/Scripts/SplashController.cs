using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour {

    [SerializeField]
    private GameObject m_intro, m_selection, m_settings;

    public AudioClip menu;

    private void Start()
    {
        AudioController.Instance.Play(menu, AudioController.SoundType.Music);
    }
    
    public void OpenSelection()
    {
        m_intro.SetActive(false);
        m_selection.SetActive(false);
        m_selection.SetActive(true);
    }

    public void OpenSettings()
    {
        m_selection.SetActive(false);
        m_settings.SetActive(true);
    }
    
    public void OnBackFromSelection()
    {
        m_selection.SetActive(false);
        m_intro.SetActive(true);
    }

    public void OnBackFromSettings()
    {
        m_settings.SetActive(false);
        m_selection.SetActive(true);
    }

}
