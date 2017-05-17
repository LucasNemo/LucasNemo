﻿using System;
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


    public Notes NotesCanvas; 

    [SerializeField]
    private DetectiveHunchBehaviour m_detectiveHunchBehaviour;
    
    [SerializeField]
    private ResultBehaviour m_resultScreen; 

    [SerializeField]
    private GameObject m_menu;
    TimerController m_timerController;
    private bool m_initialized;

    public GameObject mainCanvas; 

    private void Awake()
    {
        if (Notes.instance == null)
        {
            Instantiate(NotesCanvas);
            Notes.instance.Hide();
        }
        
        DetectiveManager.Instance.Dummy();
    }

    public AudioClip trilha; 

    private void Start()
    {
        AudioController.Instance.Play(trilha, AudioController.SoundType.Music);

        //if (Notes.instance && Manager.Instance.MyGameInformation != null)
        //    Notes.instance.Show();
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
            case Enums.DetectiveState.ReadCharacter:
                ReadQRFromsCene(false, (x) =>
                {
                    Notes.instance.gameObject.SetActive(true);
                    Notes.instance.Show();
                    mainCanvas.SetActive(true);
                    DetectiveManager.Instance.RequestChangeState(Enums.DetectiveState.StartGame);
                }, Manager.Instance.READ_CHARACTER);
                DetectiveManager.Instance.RequestChangeState(Enums.DetectiveState.WaitingForAction);
                break;
        }
    }
    
    private void InitializePlayerInfo()
    {
        ReadQRFromsCene(true,(result)=>
        {
            
        
            //DetectiveManager.Instance.RequestChangeState(Enums.DetectiveState.StartGame);
            DetectiveManager.Instance.QRCodeReaded(result, (success) =>
            {
                SceneManager.UnloadScene(Manager.Instance.QRCODE_SCENE);

                if (success)
                {
                    Notes.instance.Show();
                    mainCanvas.SetActive(true);

                   /* GenericModal.Instance.OpenAlertMode("Agora escolha seu personagem!", "Ok", () =>
                    {
                        DetectiveManager.Instance.RequestChangeState(Enums.DetectiveState.ReadCharacter);
                    });*/
                }
                else
                {
                    GenericModal.Instance.OpenAlertMode(Manager.Instance.READ_FROM_XERIFE, "Ok", () =>
                    {
                        InitializePlayerInfo();
                    });


                }
                // GenericModal.Instance.OpenAlertMode("Agora escolha seu personagem!", "Ok", () =>
                //{
                //    DetectiveManager.Instance.RequestChangeState(Enums.DetectiveState.ReadCharacter);
                //});

            });

        }, Manager.Instance.READ_FROM_XERIFE);
    }

    private void ReadQRFromsCene(bool useCompression, Action<string> result, string title)
    {
        Notes.instance.gameObject.SetActive(false);
        mainCanvas.SetActive(false);
        var readQRCodeBehaviour = FindObjectOfType<ReadQRCodeBehaviour>();
        readQRCodeBehaviour.ReadQrCode(result,  useCompression, title);
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
            mainCanvas.SetActive(true);
            var enumTest = Enum.Parse(typeof(Enums.Places), result);
            if (enumTest != null && Manager.Instance.Places.Any(x => x.MP.Equals(enumTest.GetHashCode())))
            {
                Manager.Instance.ActiveRoom = Manager.Instance.MyGameInformation.Rs.FirstOrDefault(x => x.P.MP == enumTest.GetHashCode());

                if (Manager.Instance.ActiveRoom.P.IH == 1)
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

            //TODO - conferir nome da delegacia!!
        }, string.Format( Manager.Instance.GO_TO_PD , Manager.Instance.MyGameInformation.Rs.First(x=>x.P.IH == 1).P.N));
    }

    public void OnInvesticateClicked()
    {
        m_menu.SetActive(false);
        mainCanvas.SetActive(false);
        Notes.instance.Hide();

        ReadQRFromsCene(false, (result) =>
        {
            //mainCanvas.SetActive(true);
            //Notes.instance.Hide();

            var enumTest = Enum.Parse(typeof(Enums.Places), result);

            if (enumTest != null &&   Manager.Instance.Places.Any(x=>x.MP.Equals( enumTest.GetHashCode()  )))
            {
                Manager.Instance.ActiveRoom= Manager.Instance.MyGameInformation.Rs.FirstOrDefault(x => x.P.MP == enumTest.GetHashCode());
                SceneManager.LoadSceneAsync("ARScene");
            }
        }, Manager.Instance.READ_FROM_PLACE);
    }

    public void OnBackFromHunch()
    {
        m_detectiveHunchBehaviour.gameObject.SetActive(false);
        m_menu.SetActive(true);
    }
}
