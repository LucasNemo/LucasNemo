using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultModalBehaviour : MonoBehaviour,IBackButton {

    [SerializeField]
    private Image m_characterImage, m_weaponImage, m_placeImage;

    [SerializeField]
    private Text m_resultText;

    private Action m_cancel, m_confirm;

    void Start()
    {
        BackButtonManager.Instance.AddBackButton(this);

        Sprite[] cards = Resources.LoadAll<Sprite>(Manager.Instance.HUNCH_PATH);

        Weapon crimeWeapon = Manager.Instance.MyGameInformation.CH.HR.W;
        Character crimeCharacter = Manager.Instance.Characters.Find(c => c.MC == Manager.Instance.MyGameInformation.CH.HC);
        Place crimePlace = Manager.Instance.MyGameInformation.CH.HR.P;

        Sprite weaponSprite = Manager.Instance.ReturnCorrectImage(cards, crimeWeapon.MW);
        Sprite characterSprite = Manager.Instance.ReturnCorrectImage(cards, crimeCharacter.MC);
        Sprite placeSprite = Manager.Instance.ReturnCorrectImage(cards, crimePlace.MP);
        UpdateInformation(characterSprite, weaponSprite, placeSprite, Manager.Instance.FormatResult(crimeCharacter, crimeWeapon, crimePlace));
    }

    private void UpdateInformation(Sprite character, Sprite weapon, Sprite place, string result)
    {
        m_resultText.text = result;
        m_characterImage.sprite = character;
        m_weaponImage.sprite = weapon;
        m_placeImage.sprite = place;
    }
    
    private void OnDestroy()
    {
        BackButtonManager.Instance.RemoveBackButton(this);
    }
    
    public void OnBackToMenuButtonClicked()
    {
        Manager.Instance.ClearData();
        SceneManager.LoadScene(Manager.Instance.MENU_SCENE);
    }

    public void OnAndroidBackButtonClick()
    {
        OnBackToMenuButtonClicked();
    }
}
