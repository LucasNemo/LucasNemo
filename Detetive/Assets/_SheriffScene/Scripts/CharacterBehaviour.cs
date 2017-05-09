using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBehaviour : MonoBehaviour {
    
    [SerializeField]
    private Image m_qrcodeImage;
    [SerializeField]
    private GameObject m_finishButton;
    private GameInformation m_gameInformation;
    private SheriffController m_sheriffController;
    private ReadQRCodeBehaviour m_readQr;

    void Start () {
        m_sheriffController = FindObjectOfType<SheriffController>();
        m_gameInformation = m_sheriffController.GetGameInfo;
        if (Manager.Instance.SheriffWannaPlay)
            SheriffChooseCharacter();
        else
            MakeQrCodeForPlayersChoose();
    }
    
    private void SheriffChooseCharacter()
    {
        m_readQr = FindObjectOfType<ReadQRCodeBehaviour>();
        m_readQr.ReadQrCode((string result) => {
            int charID = 0;
            if(int.TryParse(result, out charID))
            {
                m_gameInformation.P = Manager.Instance.Characters.Find(x => x.MC == charID);
                Manager.Instance.MyGameInformation = m_gameInformation;
                Manager.Instance.SaveGameInformation();
                MakeQrCodeForPlayersChoose();
            }
        }, false, "Leia um personagem");
    }

    private void MakeQrCodeForPlayersChoose()
    {
        float timer = m_sheriffController.Timer - 5f > 0 ? m_sheriffController.Timer - 5f : 0;
        m_gameInformation.Timer = timer;
        var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(m_gameInformation);
        print(serialized);
        Sprite qrCode = GenerateQRCode.GenerateQRSprite(serialized, 256);
        m_qrcodeImage.sprite = qrCode;
        m_qrcodeImage.gameObject.SetActive(true);
        m_finishButton.gameObject.SetActive(true);
    }
    
}
