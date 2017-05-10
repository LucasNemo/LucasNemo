using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericModal : MonoBehaviour {

    [SerializeField]
    private GameObject m_modal;

    [SerializeField]
    private Text m_description, m_leftText, m_rightText;

    private Action m_leftCallback, m_rightCallback;

    public static GenericModal Instance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void OpenModal(string description, string buttonLeft, string buttonRight, Action callbackButtonLeft, Action callbackButtonRight)
    {
        m_description.text = description;
        m_leftText.text = buttonLeft;
        m_rightText.text = buttonRight;
        this.m_leftCallback = callbackButtonLeft;
        this.m_rightCallback = callbackButtonRight;
        m_modal.SetActive(true);
    }

    public void CloseModal()
    {
        m_modal.SetActive(false);
    }

    public void OnLeftClick()
    {
        if (m_leftCallback != null)
            m_leftCallback();
    }

    public void OnRightCallback()
    {
        if (m_rightCallback != null)
            m_rightCallback();
    }

}
