using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericModal : MonoBehaviour {

    [SerializeField]
    private GameObject m_modal, m_leftButton, m_rightButton, m_alertButton;

    [SerializeField]
    private Text m_description, m_leftText, m_rightText, m_alertText;

    private Action m_leftCallback, m_rightCallback, m_alertModeCallback;
    private bool m_close;

    public static GenericModal Instance;

    public EasyTween modalTween;

    private bool m_isModalOpen = false;

    private void Start()
    {
        var oldModal = GenericModal.Instance;

        if ( oldModal != null && oldModal.GetInstanceID() != this.GetInstanceID())
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void OpenModal(string description, string buttonLeft, string buttonRight, Action callbackButtonLeft, Action callbackButtonRight, bool close = true)
    {
        if (m_isModalOpen) return;
        m_isModalOpen = true;

        m_leftCallback = null;
        m_rightCallback = null;
        InvertButtons(true);
        m_description.text = description;
        m_leftText.text = buttonLeft;
        m_rightText.text = buttonRight;
        m_leftCallback = callbackButtonLeft;
        m_rightCallback = callbackButtonRight;
        m_close = close;
        m_modal.SetActive(true);

        modalTween.OpenCloseObjectAnimation();
    }

    public void OpenAlertMode(string description, string button, Action alertCallback)
    {
        if (m_isModalOpen) return;
        m_isModalOpen = true;

        m_alertModeCallback = null;
        InvertButtons(false);
        m_description.text = description;
        m_alertText.text = button;
        m_alertModeCallback = alertCallback;
        m_modal.SetActive(true);
        modalTween.OpenCloseObjectAnimation();
    }

    private void InvertButtons(bool invert)
    {
        m_leftButton.SetActive(invert);
        m_rightButton.SetActive(invert);
        m_alertButton.SetActive(!invert);
    }

    public void OnClickAlertButton()
    {
        CloseModal(m_close);
        if (m_alertModeCallback != null)
            m_alertModeCallback();

        m_alertModeCallback = null;
    }

    public void CloseModal(bool close)
    {
        m_isModalOpen = false;
        modalTween.OpenCloseObjectAnimation();
    }

    public void Hide()
    {
        m_modal.SetActive(false);
    }

    public void OnLeftClick()
    {
        CloseModal(m_close);

        if (m_leftCallback != null)
            m_leftCallback();

        m_leftCallback = null;
    }
    
    public void OnRightCallback()
    {
        CloseModal(m_close);
        if (m_rightCallback != null)
            m_rightCallback();

        m_rightCallback = null;
    }

    public void RemoveHandlers()
    {
        m_rightCallback = null;
        m_leftCallback = null;
        m_alertModeCallback = null;
    }

}
