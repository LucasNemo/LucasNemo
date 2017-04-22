using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunchQrCodeBehaviour : MonoBehaviour {

    [SerializeField]
    private Image m_qrCodeImage;
    
    public void SetQrCodeImage(PlayerHunch hunch)
    {
        m_qrCodeImage.sprite = GenerateQRCode.GenerateQRSprite(Newtonsoft.Json.JsonConvert.SerializeObject(hunch));
    }
	
}
