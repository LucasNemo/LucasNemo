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
        }
    }

    public void Dummy()
    {
        //Do some dummy stuff
    }

    public Enums.DetectiveState GetCurrentState()
    {
        return m_detectiveState;
    }

    public void QRCodeReaded(string result, Action<bool> success)
    {
        if (!string.IsNullOrEmpty(result))
        {
            try
            {
                var deserializeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GameInformation>(result);
                Manager.Instance.MyGameInformation = deserializeResult;
                Manager.Instance.SaveGameInformation();      
                m_detectiveState = Enums.DetectiveState.StartGame;

                success.Invoke(true);
            }
            catch (Exception e)
            {
                Debug.LogError("\n\n\nCrash dentro do readQRFromScene: " + e.Message);
                success.Invoke(false);

                throw;
            }
        }
        else
        {
            success.Invoke(false);

            Debug.LogError("QrCode is null");
        }
    }

    public void RequestChangeState(Enums.DetectiveState nextState)
    {
        m_detectiveState = nextState;
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
