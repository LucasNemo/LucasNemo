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

                if (result != Manager.Instance.RESULT_ERROR_BACK)
                {
                    if (success)
                    {
                        GenericModal.Instance.OpenAlertMode(Manager.Instance.QR_CODE_READ_CHARACTER, "Ok", () =>
                        {
                            ReadPlayer();
                        });
                    }
                    else
                    {
                        GenericModal.Instance.OpenAlertMode(Manager.Instance.READ_FROM_XERIFE, "Ok", () =>
                        {
                            InitializePlayerInfo();
                        });
                    }
                }
                else
                {
                    SceneManager.LoadScene(Manager.Instance.MENU_SCENE);
                }
            });

        }, Manager.Instance.READ_FROM_XERIFE);
    }

    private void ReadPlayer()
    {
        ReadQRFromsCene(false, (result) =>
        {
            SceneManager.UnloadScene(Manager.Instance.QRCODE_SCENE);

            if (result != Manager.Instance.RESULT_ERROR_BACK)
            {
                int t = 0;
                Character myCharacter = null;
                if (int.TryParse(result, out t))
                    myCharacter = Manager.Instance.Characters.FirstOrDefault(x => x.MC == int.Parse(result));
                if (myCharacter != null)
                {
                    mainCanvas.SetActive(true);

                    //Saving time
                    PlayerPrefs.SetString(Manager.Instance.PLAYER_SAVE_TIME, System.DateTime.Now.ToString());
                    PlayerPrefs.Save();
                    DetectiveManager.Instance.RequestChangeState(Enums.DetectiveState.StartGame);
                }
                else
                {
                    GenericModal.Instance.OpenAlertMode(Manager.Instance.ON_READ_CHARACTER_WRONG, Manager.Instance.WARNING_BUTTON, () =>
                    {
                        ReadPlayer();
                    });
                }
            }
            else
            {
                SceneManager.LoadScene(Manager.Instance.MENU_SCENE);
            }

        }, Manager.Instance.READ_CHARACTER);
    }

    private void ReadQRFromsCene(bool useCompression, Action<string> result, string title)
    {
        mainCanvas.SetActive(false);
        var readQRCodeBehaviour = FindObjectOfType<ReadQRCodeBehaviour>();
        readQRCodeBehaviour.ReadQrCode(result,  useCompression, title);
    }

    public void OnHunchClick()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_menu.SetActive(false);
        m_detectiveHunchBehaviour.gameObject.SetActive(true);
    }
    
    public void OnFinishHunchClicked()
    {
        m_detectiveHunchBehaviour.gameObject.SetActive(false);
         
        ReadQRFromsCene(false, (result) =>
        {
            if (result != Manager.Instance.RESULT_ERROR_BACK)
            {

                mainCanvas.SetActive(true);
                try
                {
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
                        }
                    }
                    else
                    {
                        GenericModal.Instance.OpenAlertMode("Ops! Não reconhecemos essa carta. Leia a carta da delegacia", "Ok", () =>
                        {
                            OnFinishHunchClicked();
                        });
                    }
                }
                catch
                {
                    GenericModal.Instance.OpenAlertMode("Ops! Não reconhecemos essa carta. Leia a carta da delegacia", "Ok", () =>
                    {
                        OnFinishHunchClicked();
                    });
                }
            }
            else
            {
                m_detectiveHunchBehaviour.gameObject.SetActive(true);
                mainCanvas.gameObject.SetActive(true);
                m_detectiveHunchBehaviour.GetComponent<DetectiveHunchBehaviour>().ActiveConfirmModal(true);
            }

            //TODO - conferir nome da delegacia!!
        }, string.Format( Manager.Instance.GO_TO_PD ,
        Manager.Instance.PlacesNames[(Enums.Places) Manager.Instance.MyGameInformation.Rs.First(x => x.P.IH == 1).P.MP]));
    }

    public void OnInvesticateClicked()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_menu.SetActive(false);
        mainCanvas.SetActive(false);

        ReadQRFromsCene(false, (result) =>
        {
            SceneManager.UnloadScene(Manager.Instance.QRCODE_SCENE);

            if (result != Manager.Instance.RESULT_ERROR_BACK)
            {
                try
                {
                    var enumTest = Enum.Parse(typeof(Enums.Places), result);

                    if (enumTest != null && Manager.Instance.Places.Any(x => x.MP.Equals(enumTest.GetHashCode())))
                    {
                        Manager.Instance.ActiveRoom = Manager.Instance.MyGameInformation.Rs.FirstOrDefault(x => x.P.MP == enumTest.GetHashCode());
                        SceneManager.LoadSceneAsync("ARScene");
                    }
                    else
                    {
                        GenericModal.Instance.OpenAlertMode("Ops! Não reconhecemos essa carta. Leita uma carta de cenário para iniciar a investigação!", "Ok", () =>
                        {
                            OnInvesticateClicked();
                        });
                    }
                }
                catch
                {
                    GenericModal.Instance.OpenAlertMode("Ops! Não reconhecemos essa carta. Leita uma carta de cenário para iniciar a investigação!", "Ok", () =>
                    {
                        OnInvesticateClicked();
                    });
                }
            }
            else
            {
                m_menu.SetActive(true);
                mainCanvas.SetActive(true);
            }
        }, Manager.Instance.READ_FROM_PLACE);
    }

    public void OnBackFromHunch()
    {
        m_detectiveHunchBehaviour.gameObject.SetActive(false);
        m_menu.SetActive(true);
    }
}
