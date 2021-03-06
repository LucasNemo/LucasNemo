﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadQRCodeBehaviour : MonoBehaviour, IBackButton {

    private ReadQRCode readQRCode;
    private string qrCodeMessage;
    private float time = 0;
    private System.Action<string> m_readCallback;

    private string m_title;

    private enum State
    {
        none,
        readQRCode,
        QRCodeReaded
    }   

    private State m_state;
    private Coroutine m_readQRCodeRoutine;

    public void ReadQrCode(System.Action<string> callback, bool useCompression, string titleText)
    {
        BackButtonManager.Instance.AddBackButton(this);
        readQRCode = new ReadQRCode();
        m_title = titleText;
        m_state = State.readQRCode;
        m_readCallback = callback;
        ReadQR(useCompression);
    }

    private void ReadQR(bool useCompression)
    {
        readQRCode.ReadQR(this, (string e) =>
        {
            Debug.Log("Leitura do qRCODe: " + e);
            qrCodeMessage = e;
            if (!string.IsNullOrEmpty(qrCodeMessage))
            {
                if (m_readCallback != null)
                {
                    m_readCallback(qrCodeMessage);
                    m_readCallback = null;
                    BackButtonManager.Instance.RemoveBackButton(this);
                }

                m_state = State.none;
            }
        }, m_title, useCompression);
    }

    private void Escape()
    {
        if (m_readCallback != null)
        {
            m_readCallback("-1");
            BackButtonManager.Instance.RemoveBackButton(this);
        }
    }
    
    public void OnAndroidBackButtonClick()
    {
        Escape();
    }
}
