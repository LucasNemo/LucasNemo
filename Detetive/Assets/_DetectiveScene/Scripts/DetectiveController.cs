using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Handle the detective scene only - Not like the manager that handle all the detective actions
/// </summary>
public class DetectiveController : MonoBehaviour {
     
  
    [SerializeField]
    private DetectiveHunchBehaviour m_detectiveHunchBehaviour;
    
    [SerializeField]
    private ResultBehaviour m_resultScreen; 

    [SerializeField]
    private GameObject m_menu;
    TimerController m_timerController;
    private bool m_initialized;

    private void Awake()
    {
        DetectiveManager.Instance.Dummy();
    }
    
    private void FixedUpdate()
    {
        switch (DetectiveManager.Instance.GetCurrentState())
        {
            case Enums.DetectiveState.ReadGameConfiguration:
                InitializePlayerInfo();
                DetectiveManager.Instance.RequestChangeState(Enums.DetectiveState.ReadingGameState);
                break;
            case Enums.DetectiveState.StartGame:
                m_menu.SetActive(true);
                break;
            case Enums.DetectiveState.WaitingForAction:
                break;
            case Enums.DetectiveState.Investigate:
                break;
            case Enums.DetectiveState.Investigating:
                break;
            case Enums.DetectiveState.EndingInvestigation:
                break;
            case Enums.DetectiveState.Hunch:
                break;
            case Enums.DetectiveState.WaitingDeploy:
                break;
            case Enums.DetectiveState.ResultScreen:
                break;
            case Enums.DetectiveState.ReadingGameState:
                break;
        }
    }
    
    private void InitializePlayerInfo()
    {
        ReadQRFromsCene(true,(result)=>
        {
            DetectiveManager.Instance.QRCodeReaded(result);
        }, Manager.Instance.READ_FROM_XERIFE);
    }

    private void ReadQRFromsCene(bool useCompression, Action<string> result, string title)
    {
        var readQRCodeBehaviour = FindObjectOfType<ReadQRCodeBehaviour>();

        readQRCodeBehaviour.ReadQrCode(result,
         useCompression, title);
    }

    public void OnHunchClick()
    {
        m_menu.SetActive(false);
        m_detectiveHunchBehaviour.gameObject.SetActive(true);
    }
    
    public void OnFinishHunchClicked()
    {
        m_detectiveHunchBehaviour.gameObject.SetActive(false);
         
        ReadQRFromsCene(false, (result) =>
        {
            var enumTest = Enum.Parse(typeof(Enums.Places), result);

            if (enumTest != null && Manager.Instance.Places.Any(x => x.MP.Equals(enumTest.GetHashCode())))
            {
                Manager.Instance.ActiveRoom = Manager.Instance.MyGameInformation.Rs.FirstOrDefault(x => x.P.MP == enumTest.GetHashCode());

                if (Manager.Instance.ActiveRoom.P.IH)
                {
                    var playerhunch = m_detectiveHunchBehaviour.GetHunch.MH;
                    var answer = Manager.Instance.MyGameInformation.CH == playerhunch;

                    m_resultScreen.gameObject.SetActive(true);
                    m_resultScreen.UpdateInformation(answer);
                       
                    //m_resultScreen.GetComponentInChildren<Text>().text = string.Format("Seu palpite\nSuspeito{0}\nLocal{1}\nArma{2}\n\nEstá {3}",
                    //     ((Enums.Characters)playerhunch.HC).ToString(), (playerhunch.HR.P.N),
                    //     ((Enums.Weapons)playerhunch.HR.W.MW).ToString(), answer ? "CORRETA!!!" : "ERRADO!"  );
                }
            }
        }, Manager.Instance.GO_TO_PD);
    }

    public void OnInvesticateClicked()
    {
        m_menu.SetActive(false);
        
        ReadQRFromsCene(false, (result) =>
        {
            var enumTest = Enum.Parse(typeof(Enums.Places), result);

            if (enumTest != null &&   Manager.Instance.Places.Any(x=>x.MP.Equals( enumTest.GetHashCode()  )))
            {
                Manager.Instance.ActiveRoom = Manager.Instance.MyGameInformation.Rs.FirstOrDefault(x => x.P.MP == enumTest.GetHashCode());
                SceneManager.LoadScene("ARScene");
            }
        }, Manager.Instance.READ_FROM_PLACE);
    }

    public void OnBackFromHunch()
    {
        m_detectiveHunchBehaviour.gameObject.SetActive(false);
        m_menu.SetActive(true);
    }
}
