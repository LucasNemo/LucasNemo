using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class Notes : MonoBehaviour, IBackButton {

    [SerializeField]
    private List<GameObject> m_pages;

    [Space(10)]
    [SerializeField]
    private GameObject m_notes;

    public GameObject mainPage;

    [SerializeField]
    private List<Image> m_buttonsImages;
    public static Notes instance;
    public EasyTween tween;
    public Animator animator;

    private readonly float delay = 0.3f;

    public bool IsOpen
    {
        get
        {
            return m_notes.activeSelf;
        }
    }


    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            tween.animationParts.ExitEvents.AddListener(exitAnimatonCompleted);
            tween.animationParts.IntroEvents.AddListener(onIntroAnimationCompleted);
            tween.animationParts.CallCallback = UITween.AnimationParts.CallbackCall.END_OF_INTRO_AND_END_OF_EXIT_ANIM;
        }
    }

    private void onIntroAnimationCompleted()
    {
        mainPage.SetActive(true);
        BackButtonManager.Instance.AddBackButton(this);
    }

    private void exitAnimatonCompleted()
    {
        ChangePage(0);
        m_notes.SetActive(false);

        foreach (var item in m_pages)
        {
            item.SetActive(false);
        }

        BackButtonManager.Instance.RemoveBackButton(this);
    }
    
    public void NotesClicked()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_notes.SetActive(true);
        tween.OpenCloseObjectAnimation();
    }

    public void Remove()
    {
        instance = null;
        Destroy(gameObject);   
    }
    
    public void Show()
    {
        m_notes.SetActive(true);
        tween.OpenCloseObjectAnimation();
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

    public void OnAndroidBackButtonClick()
    {
        Hide();
    }
}
