using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionItem : GenericSelectItem<Weapon> {

    [SerializeField]
    private Text m_weaponNameText;

    [SerializeField]
    private Image m_weaponImage;

    public override void UpdateItem(Weapon Item, Action<Weapon> callback)
    {
        base.UpdateItem(Item, callback);
        m_weaponNameText.text =  Manager.Instance.WeaponsName[ (Enums.Weapons) Item.MW ];
    }


    public void UpdateSprite(Sprite currentSprite)
    {
        m_weaponImage.sprite = currentSprite;
    }

    public override void SelectItem()
    {
        m_weaponNameText.color = Color.red;

        base.SelectItem();
    }

    public override void UnSelectItem()
    {
        base.UnSelectItem();
        m_weaponNameText.color = Color.black;
    }
}
