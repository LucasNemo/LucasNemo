using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        ReadQRFromsCene(true,(result)=>
        {
            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    var deserializeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GameInformation>(result);
                    Manager.Instance.MyGameInformation = deserializeResult;
                    m_menu.SetActive(true);
                }
                catch (Exception e)
                {
                    print(e.Message);
                    throw;
                }

            }
            else
            {
                print("QrCode is null");
            }
        });
    
    }

    private void ReadQRFromsCene(bool useCompression, Action<string> result)
    {
        FindObjectOfType<ReadQRCodeBehaviour>().ReadQrCode(result,
         useCompression);
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

    public void OnInvesticateClicked()
    {
        ReadQRFromsCene(false, (result) =>
        {
            var enumTest = Enum.Parse(typeof(Enums.Places), result);

            if (enumTest != null && 

            Manager.Instance.Places.Any(x=>x.MP.Equals( enumTest  )))
            {
                SceneManager.LoadScene("ARTest");
            }
        });


        //SceneManager.LoadScene("ARTest"); 
    }

}
