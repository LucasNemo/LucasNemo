﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Notes : MonoBehaviour {

    [SerializeField]
    private List<GameObject> m_pages;
    [SerializeField]
    private GameObject m_notes;
    public static Notes instance;
    public EasyTween tween;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Animator animator;

    public bool IsOpen {
        get
        {
            return m_notes.activeSelf;
        }
    }

    public void NotesClicked()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        tween.OpenCloseObjectAnimation();
        m_notes.SetActive(!m_notes.activeSelf);
    }

    public void Remove()
    {
        instance = null;
        Destroy(gameObject);   
    }
    
    public void Show()
    {
        tween.OpenCloseObjectAnimation();
        m_notes.SetActive(true);
    }

    public void Hide()
    {
        tween.OpenCloseObjectAnimation();
        m_notes.SetActive(false);
    }

    public void ChangePage(int page)
    {
        m_pages.ForEach(x => x.SetActive(false));
        m_pages[page].SetActive(true);
    }
}
