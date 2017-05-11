using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmHunchBehaviour : MonoBehaviour {

    [SerializeField]
    private Image m_characterImage, m_weaponImage, m_placeImage;

    [SerializeField]
    private Text m_resultText;

    private Action m_cancel, m_confirm;

    public void UpdateInformation(Sprite character, Sprite weapon, Sprite place, string result,Action cancelCallback, Action confirmCallback)
    {
        m_resultText.text = result;
        m_characterImage.sprite = character;
        m_weaponImage.sprite = weapon;
        m_placeImage.sprite = place;
        m_cancel = cancelCallback;
        m_confirm = confirmCallback;
    }

    public void OnCancelClick()
    {
        if (m_cancel != null)
            m_cancel();
    }

    public void OnConfirmClick()
    {
        if (m_confirm != null)
            m_confirm();
    }

}
