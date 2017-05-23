using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class Notes : MonoBehaviour {

    [SerializeField]
    private List<GameObject> m_pages;
    [SerializeField]
    private GameObject m_notes;
    [SerializeField]
    private List<Image> m_buttonsImages;

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
            tween.animationParts.ExitEvents.AddListener(exitAnimatonCompleted);
            tween.animationParts.CallCallback = UITween.AnimationParts.CallbackCall.END_OF_EXIT_ANIM;
        }
    }

    private void exitAnimatonCompleted()
    {
        m_notes.SetActive(false);
    }

    public Animator animator;

    public bool IsOpen {
        get
        {
            return m_notes.activeSelf;
        }
    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsOpen)
        {
            Hide();
        }
    }

    public void NotesClicked()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        tween.OpenCloseObjectAnimation();
        m_notes.SetActive(true);
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
    }

    public void ChangePage(int page)
    {
        m_pages.ForEach(x => x.SetActive(false));
        m_buttonsImages.ForEach(x => x.color = GetNormalColor());
        m_buttonsImages[page].color = GetSelectedColor();
        m_pages[page].SetActive(true);
    }
    
    private Color32 GetSelectedColor()
    {
        return new Color32(32, 52, 43, 255);
    }

    private Color32 GetNormalColor()
    {
        return new Color32(56, 107, 92, 255);
    }
}
