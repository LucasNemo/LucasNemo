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

    public static GenericModal Instance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void OpenModal(string description, string buttonLeft, string buttonRight, Action callbackButtonLeft, Action callbackButtonRight)
    {
        InvertButtons(true);
        m_description.text = description;
        m_leftText.text = buttonLeft;
        m_rightText.text = buttonRight;
        this.m_leftCallback = callbackButtonLeft;
        this.m_rightCallback = callbackButtonRight;
        m_modal.SetActive(true);
    }

    public void OpenAlertMode(string description, string button, Action alertCallback)
    {
        InvertButtons(false);
        m_description.text = description;
        m_alertText.text = button;
        m_alertModeCallback = alertCallback;
        m_modal.SetActive(true);
    }

    private void InvertButtons(bool invert)
    {
        m_leftButton.SetActive(invert);
        m_rightButton.SetActive(invert);
        m_alertButton.SetActive(!invert);
    }

    public void OnClickAlertButton()
    {
        if (m_alertModeCallback != null)
            m_alertModeCallback();

        CloseModal();
    }

    private void CloseModal()
    {
        m_modal.SetActive(false);
    }

    public void OnLeftClick()
    {
        if (m_leftCallback != null)
            m_leftCallback();

        CloseModal();
    }

    public void OnRightCallback()
    {
        if (m_rightCallback != null)
            m_rightCallback();

        CloseModal();
    }

}
