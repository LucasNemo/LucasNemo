using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadQRCode
{
    string qrCodeSceneName = "QRScanScene";

    private MonoBehaviour sender;
    private Action<string> m_callback;
    private bool m_zipped;
    private QRCodeDecodeController m_qrCodeDecoder;

    public void ReadQR(MonoBehaviour sender, Action<string> callback, string title, bool zip = true)
    {
        this.sender = sender;
        m_callback = callback;
        m_zipped = zip;
        sender.StartCoroutine(ReadQRCodeAsync(title));
    }

    IEnumerator ReadQRCodeAsync(string title)
    {
        yield return SceneManager.LoadSceneAsync(qrCodeSceneName, LoadSceneMode.Additive);

        CheckQRCodeReferente();

        m_qrCodeDecoder.SetSceneTitle(title);

        m_qrCodeDecoder.onQRScanFinished += ReadQRCode_onQRScanFinished;
    }

    private void CheckQRCodeReferente()
    {
        if (m_qrCodeDecoder == null)
            m_qrCodeDecoder = GameObject.FindObjectOfType<QRCodeDecodeController>();
    }

    private void ReadQRCode_onQRScanFinished(string str)
    {
        sender.StartCoroutine(QRCodeResultCompleted(str));
    }

    IEnumerator QRCodeResultCompleted(string str)
    {
        CheckQRCodeReferente();
        m_qrCodeDecoder.onQRScanFinished -= ReadQRCode_onQRScanFinished;
        m_qrCodeDecoder.StopWork();
        m_qrCodeDecoder = null;
        string result = m_zipped ? Helper.DecompressString(str) : str;
        m_callback.Invoke(result);
        m_callback = null;
        yield return null;

        try
        {
            SceneManager.UnloadScene(qrCodeSceneName);
        }catch
        {

        }
     //   yield return SceneManager.UnloadSceneAsync(qrCodeSceneName);
    }
}
