using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveManager : SingletonBehaviour<DetectiveManager> {
    
    private Enums.DetectiveState m_detectiveState;
    private float m_timer; 
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Manager.Instance.MyGameInformation == null)
        {
            TryLoadPreviousGameData();
            m_detectiveState = Enums.DetectiveState.ReadGameConfiguration;
        }
    }

    private void TryLoadPreviousGameData()
    {
        //throw new NotImplementedException();
    }

    void Update()
    {
        switch (m_detectiveState)
        {
            case Enums.DetectiveState.ReadGameConfiguration:
                HandleReadGameConfiguration();
                break;
            case Enums.DetectiveState.StartGame:
                HandleStartGame();
                break;
            case Enums.DetectiveState.WaitingForAction:
                HandleWaitingForAction();
                break;
            case Enums.DetectiveState.Investigate:
                HandleInvestigate();
                break;
            case Enums.DetectiveState.Investigating:
                HandleInvestigating();
                break;
            case Enums.DetectiveState.EndingInvestigation:
                HandleEndingInvestigation();
                break;
            case Enums.DetectiveState.Hunch:
                HandleHunch();
                break;
            case Enums.DetectiveState.WaitingDeploy:
                HandleWaitingDeploy();
                break;
            case Enums.DetectiveState.ResultScreen:
                HandleResultScreen();
                break;
            case Enums.DetectiveState.TimerToStart:
                m_timer += Time.deltaTime; 
                //TODO - FOR THE LOVE OF THE GOD, REMOVE THE HARDCODED NUMBER!!!!
                if (m_timer > 120f)
                {
                    m_timer = 0;
                    RequestChangeState(Enums.DetectiveState.WaitingForAction);
                }
                break;
            default:
                break;
        }
    }

    public Enums.DetectiveState GetCurrentState()
    {
        return m_detectiveState;
    }

    public void QRCodeReaded(string result)
    {
        if (!string.IsNullOrEmpty(result))
        {
            try
            {
                var deserializeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GameInformation>(result);
                Manager.Instance.MyGameInformation = deserializeResult;

                //TODO - Save game data!               
                m_detectiveState = Enums.DetectiveState.TimerToStart;
            }
            catch (Exception e)
            {
                Debug.LogError("\n\n\nCrash dentro do readQRFromScene: " + e.Message);
                throw;
            }
        }
        else
        {
            Debug.LogError("QrCode is null");
        }
    }

    public void RequestChangeState(Enums.DetectiveState nextState)
    {
        m_detectiveState = nextState;
    }
    
    private void HandleResultScreen()
    {
        throw new NotImplementedException(); 
    }

    private void HandleWaitingDeploy()
    {
        throw new NotImplementedException();
    }

    private void HandleHunch()
    {
        throw new NotImplementedException();
    }

    private void HandleEndingInvestigation()
    {
        throw new NotImplementedException();
    }

    private void HandleInvestigating()
    {
        throw new NotImplementedException();
    }

    private void HandleInvestigate()
    {
        throw new NotImplementedException();
    }

    private void HandleWaitingForAction()
    {
        throw new NotImplementedException();
    }

    private void HandleStartGame()
    {
         // do some stat stuff 
    }

    private void HandleReadGameConfiguration()
    {
        throw new NotImplementedException();
    }
}
