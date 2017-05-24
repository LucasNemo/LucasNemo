using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectiveManager : SingletonBehaviour<DetectiveManager> {
    
    private Enums.DetectiveState m_detectiveState;
    private float m_timer;


    private void Awake()
    {
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
}
