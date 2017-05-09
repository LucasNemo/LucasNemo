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
    private HunchQrCodeBehaviour m_hunchQr;

    [SerializeField]
    private GameObject m_menu;

    private bool m_initialized;
    
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
            case Enums.DetectiveState.TimerToStart:
                break;
        }
    }

    private void InitializePlayerInfo()
    {
        ReadQRFromsCene(true,(result)=>
        {
            DetectiveManager.Instance.QRCodeReaded(result);
        });
    }

    private void ReadQRFromsCene(bool useCompression, Action<string> result)
    {
        var readQRCodeBehaviour = FindObjectOfType<ReadQRCodeBehaviour>();

        var camTexture = readQRCodeBehaviour.GetCameraTexture;

        readQRCodeBehaviour.ReadQrCode(result,
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

            if (enumTest != null &&   Manager.Instance.Places.Any(x=>x.MP.Equals( enumTest.GetHashCode()  )))
            {
                Manager.Instance.ActiveRoom = Manager.Instance.MyGameInformation.Rs.FirstOrDefault(x => x.P.MP == enumTest.GetHashCode());
                SceneManager.LoadScene("ARScene");
            }
        });
    }

}
