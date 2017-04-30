using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadQRCodeBehaviour : MonoBehaviour {

    private WebCamTexture camTexture;
    private Rect screenRect;
    private ReadQRCode readQRCode;
    private string qrCodeMessage;
    private float time = 0;
    private System.Action<string> m_readCallback;

    public GameObject cameraCanvasPrefab;

    private GameObject m_cameraCanvasReference;
    private Image m_cameraImage;

    private enum State
    {
        none,
        readQRCode,
        QRCodeReaded
    }   

    private State m_state;
    private Coroutine m_readQRCodeRoutine;

    private void Awake()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        readQRCode = new ReadQRCode();
    }

    void Start() {
        InitializeCamera();
    }

    void InitializeCamera()
    {
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = 400;
        camTexture.requestedWidth = 400;
    }

    public void ReadQrCode(System.Action<string> callback, bool useCompression)
    {
        if (camTexture == null)
            InitializeCamera();

        camTexture.Play();

        if (m_cameraCanvasReference == null)
        {
            GetCameraCanvasReference();
        }

        m_cameraImage.material.mainTexture = camTexture;

        m_readQRCodeRoutine = StartCoroutine(ReadQRCode(useCompression));
        m_state = State.readQRCode;
        m_readCallback = callback;
    }

    private void GetCameraCanvasReference()
    {
        m_cameraCanvasReference = Instantiate(cameraCanvasPrefab);
        m_cameraImage = m_cameraCanvasReference.GetComponentInChildren<Image>();
    }

    public WebCamTexture GetCameraTexture
    {
        get
        {
            return camTexture;
        }
    }


    //void OnGUI()
    //{ 
    //    switch (m_state)
    //    {
    //        case State.none:
    //            break;
    //        case State.readQRCode:
    //            // drawing the camera on screen
    //            GUIUtility.RotateAroundPivot(camTexture.videoRotationAngle, Vector2.zero);
    //            GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
    //            break;
    //    }
    //}

    IEnumerator ReadQRCode(bool useCompression)
    {
        do
        {
            yield return new WaitForSeconds(.1f);
            if (camTexture != null)
            {

                if (m_cameraImage)
                    m_cameraImage.transform.localRotation = Quaternion.Euler(0, 0, ((WebCamTexture)(m_cameraImage.material.mainTexture)).videoRotationAngle);

                readQRCode.ReadQR(camTexture, (string e) =>
                {
                    Destroy(m_cameraCanvasReference);
                    m_cameraCanvasReference = null;

                    qrCodeMessage = e;

                    Debug.LogError("\n\n\n\n\n\n\n\n" + e + "\n\n\n\\n\n");

                    if (!string.IsNullOrEmpty(qrCodeMessage))
                    {
                        if (m_readCallback != null)
                            m_readCallback(qrCodeMessage);
                        m_state = State.none;
                        camTexture.Stop();
                        StopCoroutine(m_readQRCodeRoutine);
                    }
                },useCompression);
            }
        } while (true);

    }
}
