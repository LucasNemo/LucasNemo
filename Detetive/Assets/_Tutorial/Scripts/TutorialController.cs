﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    static string tutorialText;
    static TutorialStage tutorialStage;

    public Text content;
    public EasyTween tween;
    public GameObject perfilPanel, readQRCodePanel, xerifePanel;

    public static TutorialController instance;

    public static void Show(string text, TutorialStage stage)
    {
        tutorialStage = stage;
        tutorialText = text;
        SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive);
    }

    public static void Close()
    {
        if (instance)
        instance.OnClick();

    }

    private void Start()
    {
        instance = this;
        content.text = tutorialText;
        tween.OpenCloseObjectAnimation();

        switch (tutorialStage)
        {
            case TutorialStage.Perfil:
                perfilPanel.SetActive(true);
                break;
            case TutorialStage.LER_QR_CODE:
                readQRCodePanel.SetActive(true);
                break;
            case TutorialStage.QRCode_Xerife:
                readQRCodePanel.SetActive(true);
                break;
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        { OnClick(); }
    }


    #region Events

    public void OnClick()
    {

        TutorialController.tutorialText = "";

        if (!tween) return;

        tween.animationParts.ExitEvents.AddListener(OnExitAnimation);
        tween.OpenCloseObjectAnimation();
        
    }

    private void OnExitAnimation()
    {
        switch (tutorialStage)
        {
            case TutorialStage.Perfil:
                perfilPanel.SetActive(false);
                break;
            case TutorialStage.LER_QR_CODE:
                readQRCodePanel.SetActive(false);
                break;
            case TutorialStage.QRCode_Xerife:
                readQRCodePanel.SetActive(false);
                //xerifePanel.SetActive(false);
                break;
        }

        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }


    #endregion

    public enum TutorialStage
    {
        Perfil,
        LER_QR_CODE,
        QRCode_Xerife
        
    }

}
