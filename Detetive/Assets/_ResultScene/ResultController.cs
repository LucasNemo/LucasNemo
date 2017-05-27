using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultController : MonoBehaviour,IBackButton {

    private static bool m_win;
    [SerializeField]
    private GameObject m_winPanel, m_loosePanel;
    [SerializeField]
    private Text m_winDescription, m_looseResult;
    [SerializeField]
    private Image m_characterImage, m_weaponImage, m_placeImage;
    [SerializeField]
    private AudioClip winSound, looseSound;

    public static void Show(bool win)
    {
        m_win = win;
        SceneManager.LoadScene(Manager.Instance.RESULT_SCENE);
        
    }

	void Awake () {
        BackButtonManager.Instance.AddBackButton(this);

        if (m_win) HandleWin();
        else HandleLoose();
	}
	
    private void HandleWin()
    {
        AudioController.Instance.ChangeVolume(AudioController.SoundType.Music, 0f);
        AudioController.Instance.Play(winSound, AudioController.SoundType.SoundEffect2D);
        TimeSpan span = Manager.Instance.GetTime();
        m_winDescription.text = string.Format(Manager.Instance.WIN_DESCRIPTION, span.Hours, span.Minutes);

        m_winPanel.SetActive(true);

        //Remove all informations after see result
        Manager.Instance.ClearData();
    }

    private void HandleLoose()
    {
        AudioController.Instance.ChangeVolume(AudioController.SoundType.Music, 0f);
        AudioController.Instance.Play(looseSound, AudioController.SoundType.SoundEffect2D);
        Sprite[] cards = Resources.LoadAll<Sprite>(Manager.Instance.HUNCH_PATH);

        Weapon crimeWeapon = Manager.Instance.MyGameInformation.CH.HR.W;
        Character crimeCharacter = Manager.Instance.Characters.Find(c => c.MC == Manager.Instance.MyGameInformation.CH.HC);
        Place crimePlace = Manager.Instance.MyGameInformation.CH.HR.P;

        Sprite weaponSprite = Manager.Instance.ReturnCorrectImage(cards, crimeWeapon.MW);
        Sprite characterSprite = Manager.Instance.ReturnCorrectImage(cards, crimeCharacter.MC);
        Sprite placeSprite = Manager.Instance.ReturnCorrectImage(cards, crimePlace.MP);
        UpdateInformation(characterSprite, weaponSprite, placeSprite, Manager.Instance.FormatResult(crimeCharacter, crimeWeapon, crimePlace));

        m_loosePanel.SetActive(true);

        //Remove all informations after see result
        Manager.Instance.ClearData();
    }

    private void UpdateInformation(Sprite character, Sprite weapon, Sprite place, string result)
    {
        m_looseResult.text = result;
        m_characterImage.sprite = character;
        m_weaponImage.sprite = weapon;
        m_placeImage.sprite = place;
    }

    private void OnDestroy()
    {
        AudioController.Instance.ChangeVolume(AudioController.SoundType.Music, 1);
        BackButtonManager.Instance.RemoveBackButton(this);
    }

    public void OnAndroidBackButtonClick()
    {
        //Remove all informations after see result
        Manager.Instance.ClearData();
        SceneManager.LoadScene(Manager.Instance.MENU_SCENE);
    }
    
}
