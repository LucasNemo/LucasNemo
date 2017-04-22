using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectiveController : MonoBehaviour {

    [SerializeField]
    private DetectiveHunchBehaviour m_detectiveHunchBehaviour;
    [SerializeField]
    private HunchQrCodeBehaviour m_hunchQr;
    [SerializeField]
    private GameObject m_menu;

    private void Start()
    {
        if(Manager.Instance.MyGameInformation == null)
        {
            InitializePlayerInfo();
        }
        else
        {
            m_menu.SetActive(true);
        }
    }

    private void InitializePlayerInfo()
    {
        FindObjectOfType<ReadQRCodeBehaviour>().ReadQrCode((result) =>
        {
            if (!string.IsNullOrEmpty(result))
            {
                Manager.Instance.MyGameInformation = Newtonsoft.Json.JsonConvert.DeserializeObject<GameInformation>(result);
                m_menu.SetActive(true);
            }
            else
            {
                print("QrCode is null");
            }
        });
    }

    public void OnHunchClick()
    {
        m_menu.SetActive(false);
        m_detectiveHunchBehaviour.gameObject.SetActive(true);
    }
    
    public void OnFinishHunchClicked()
    {
        m_detectiveHunchBehaviour.gameObject.SetActive(false);
        m_hunchQr.SetQrCodeImage(m_detectiveHunchBehaviour.GetHunch);
        m_hunchQr.gameObject.SetActive(true);
    }

}
