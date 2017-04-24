using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadQRCodeBehaviour : MonoBehaviour {

    private WebCamTexture camTexture;
    private Rect screenRect;
    private ReadQRCode readQRCode;
    private string qrCodeMessage;
    private float time = 0;
    private System.Action<string> m_readCallback;

    private enum State
    {
        none,
        readQRCode,
        QRCodeReaded
    }

    private State m_state;

    void Start () {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = 400;
        camTexture.requestedWidth = 400;

        if (camTexture != null)
        {
            camTexture.Play();
        }

        readQRCode = new ReadQRCode();
    }

    public void ReadQrCode(System.Action<string> callback)
    {
        m_state = State.readQRCode;
        m_readCallback = callback;
    }

    void OnGUI()
    { 
        switch (m_state)
        {
            case State.none:
                break;
            case State.readQRCode:
                // drawing the camera on screen
                GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
                GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400), "");
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (m_state)
        {
            case State.none:
                break;
            case State.readQRCode:

                time += Time.deltaTime;
                if (time > .3f)
                {
                    time = 0;
                    if (camTexture != null)
                    {
                        readQRCode.ReadQR(camTexture, (string e) =>
                        {
                            qrCodeMessage = e;

                            if (!string.IsNullOrEmpty(qrCodeMessage))
                            {
                                if (m_readCallback != null)
                                    m_readCallback(qrCodeMessage);
                                m_state = State.none;
                            }
                        });
                    }
                }
                break;
            case State.QRCodeReaded:
                break;
            default:
                break;
        }
    }

}
