using System;
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

    public static void Show(string text, TutorialStage stage)
    {
        tutorialStage = stage;
        tutorialText = text;
        SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive);
    }

    private void Start()
    {
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
                xerifePanel.SetActive(true);
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
                xerifePanel.SetActive(false);
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
