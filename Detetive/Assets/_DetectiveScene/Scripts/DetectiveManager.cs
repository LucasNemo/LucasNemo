using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveManager : SingletonBehaviour<DetectiveManager> {
    
    private Enums.DetectiveState m_detectiveState;
    private float m_timer;

    public List<Enums.Places> IsPericiaRequested;

    public Dictionary<Enums.Places, bool> PericiaResults;

    public List<Enums.Places> PericiasToCheck;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (IsPericiaRequested == null)
            IsPericiaRequested = new List<Enums.Places>();

        if (PericiaResults == null)
            PericiaResults = new Dictionary<Enums.Places, bool>();

        if (PericiasToCheck == null)
            PericiasToCheck = new List<Enums.Places>();

        if (Manager.Instance.MyGameInformation == null)
        {
            m_detectiveState = Enums.DetectiveState.ReadGameConfiguration;
        }
        else
        {
            m_detectiveState = Enums.DetectiveState.StartGame;
            if (Notes.instance)
                Notes.instance.Show();
        }
    }

    public void Dummy()
    {
        //Do some dummy stuff
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
                Manager.Instance.SaveGameInformation();      
                m_detectiveState = Enums.DetectiveState.StartGame;
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
        //TODO HANDLE HERE? 
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

    public bool PericiaAlreadyRequested(Enums.Places place)
    {
        return IsPericiaRequested.Contains(place);
    }

    public void RequestPericiaToThisPlace()
    {
        var currentPlace = Manager.Instance.ActiveRoom;

        if (IsPericiaRequested.Contains( (Enums.Places) currentPlace.P.MP))
        {
            return;
        }

        IsPericiaRequested.Add( (Enums.Places) currentPlace.P.MP);
    }

    public bool AlreadGotAResultToThisPlace()
    {
        var currentPlace = Manager.Instance.ActiveRoom;

        foreach (var item in IsPericiaRequested)
        {
            if (currentPlace.P.MP != item.GetHashCode())
            {
                if (!PericiaResults.ContainsKey(item))
                {
                    var correctPlace = Manager.Instance.MyGameInformation.CH.HR.P.MP;
                    PericiaResults.Add(item, correctPlace == item.GetHashCode());

                    if (!PericiasToCheck.Contains(item))
                        PericiasToCheck.Add(item);
                }
            }
        }

        if (PericiaResults.ContainsKey((Enums.Places)currentPlace.P.MP))
            return true;

        return false;
    }

    public bool PericiaResultToThisPlace()
    {
        var currentPlace = Manager.Instance.ActiveRoom;

        return PericiaResults[(Enums.Places)currentPlace.P.MP];
    }

}
