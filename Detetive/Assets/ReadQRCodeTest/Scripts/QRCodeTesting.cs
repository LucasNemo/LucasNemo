using ZXing;
using ZXing.QrCode;
using UnityEngine;
using System;

public class QRCodeTesting : MonoBehaviour
{
    private WebCamTexture camTexture;
    private Rect screenRect;
    private Texture2D myQRCodeGenerated;
    string qrCodeMessage;

    ReadQRCode readQRCode; 

    private enum State
    {
        none,
        readQRCode,
        QRCodeReaded,
        GenerateQRCode
    }
    private State m_state;

    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
        if (camTexture != null)
        {
            camTexture.Play();
        }

        readQRCode = new ReadQRCode();
    }
    private void FixedUpdate()
    {
        switch (m_state)
        {
            case State.none:
                break;
            case State.readQRCode:

                time += Time.deltaTime;
                if (time > 1)
                {
                    time = 0;
                    if (camTexture != null)
                    {
                        readQRCode.ReadQR(camTexture, (string e) =>
                        {
                            qrCodeMessage = e;

                            if (!string.IsNullOrEmpty(qrCodeMessage))
                                m_state = State.QRCodeReaded;
                        });
                    }
                }
                break;
            case State.QRCodeReaded:
                break;
            case State.GenerateQRCode:
                break;
            default:
                break;
        }
    }

    float time = 0;
    void OnGUI()
    {
        DrawInitialMenu();

        switch (m_state)
        {
            case State.none:
                break;
            case State.readQRCode:
                // drawing the camera on screen
                GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
                break;
            case State.QRCodeReaded:
                GUI.Label(new Rect(100, 100, 200, 200), "Resultado do QR:"+ qrCodeMessage);
                break;
            case State.GenerateQRCode:
                //Drawing the QRCode
                if (GUI.Button(new Rect(Screen.width / 2 - 256 / 2, Screen.height / 2 - 256 / 2, 256, 256), myQRCodeGenerated, GUIStyle.none)) { }
                break;
        }

    }

    int count = 0; 

    private void DrawInitialMenu()
    {
        if (GUI.Button(new Rect(0, 0, 100, 35), "Criar QR"))
        {
            m_state = State.GenerateQRCode;
            myQRCodeGenerated = GenerateQRCode.GenerateQR("Texto Legal dentro do QRCode!"+count);
            count++;
        }
        else if (GUI.Button(new Rect(0, 50, 100, 35), "LerQR"))
            m_state = State.readQRCode;
    }
}
