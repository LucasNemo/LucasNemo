using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARHud : MonoBehaviour, IBackButton {

    public DeviceCameraController deviceCamera;
    public GameObject periciaButton;
    public Text title;
    public AudioClip sirene;
    private bool musicStatus;
    private Enums.Places m_activePlace;

    public void Start()
    {
        BackButtonManager.Instance.AddBackButton(this);

        m_activePlace = (Enums.Places) Manager.Instance.ActiveRoom.P.MP;

        var name = Manager.Instance.PlacesNames[m_activePlace];

        //Set the name choosed by player
        title.text = Manager.Instance.ActiveRoom.P.IH == 1 ? "DELEGACIA" + name : name;

        //Check pericia
        UpdatePericia();
    }

    private void UpdatePericia()
    {
        var pericia = PericiaController.Instance.GetPericia(m_activePlace);
        if (pericia != null)
        {
            if (pericia.Status == Enums.PericiaStatus.Result)
            {
                //Show Result!

                ShowResult(pericia);

                PericiaController.Instance.Complete(pericia);
            }
        }
    }

    private void ShowResult(Pericia pericia)
    {
        var correctPlace = Manager.Instance.MyGameInformation.CH.HR.P.MP;

        if (correctPlace == pericia.Place.GetHashCode())
        {
            MurderPlace();
        }
        else
        {
            GenericModal.Instance.OpenAlertMode(Manager.Instance.PERICIA_WRONG_PLACE, "Ok", null);
        }
    }

    private void MurderPlace()
    {
        musicStatus = AudioController.Instance.IsMusicMuted;
        AudioController.Instance.MuteMusic();
        AudioController.Instance.Play(sirene, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        GenericModal.Instance.OpenAlertMode(Manager.Instance.PERICIA_CORRECT_PLACE, "Ok", null);        
        Invoke("MusicStatus", 5f);
    }


    void MusicStatus()
    {
        if (!musicStatus)
        {
            AudioController.Instance.UnMuteMusic();
            AudioController.Instance.Resume(AudioController.SoundType.Music);
        }
    }

    public void OnBackClicked()
    {
        deviceCamera.StopWork();

        var pericia = PericiaController.Instance.Run(m_activePlace);

        //We got a result!
        if (pericia != null)
        {
            GenericModal.Instance.OpenAlertMode(string.Format(Manager.Instance.PERICIA_RESULT, Manager.Instance.PlacesNames[pericia.Place]), "Ok", () =>
           {
               SceneManager.LoadScene(Manager.Instance.DETETIVE_SCENE);
           });
        }
        else
        {
            SceneManager.LoadScene(Manager.Instance.DETETIVE_SCENE);
        }
    }


    public void OnPericiaRequestClick()
    {
        //Request perica to this place
        var pericia = PericiaController.Instance.GetPericia(m_activePlace);
        if (pericia == null)
        {
            PericiaController.Instance.RequestPericia(m_activePlace);
            periciaButton.SetActive(false);
            GenericModal.Instance.OpenAlertMode(Manager.Instance.PERICIA_START, "Ok", null);
        }
        else if (pericia.Status == Enums.PericiaStatus.Done )
        {
            ShowResult(pericia);
        }
        else
        {
            GenericModal.Instance.OpenAlertMode(Manager.Instance.PERICIA_START, "Ok", null);
        }
    }

    private void OnDestroy()
    {
        BackButtonManager.Instance.RemoveBackButton(this);
    }

    public void OnAndroidBackButtonClick()
    {
        OnBackClicked();
    }
}
