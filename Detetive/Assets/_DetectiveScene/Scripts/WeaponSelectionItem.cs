using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionItem : GenericSelectItem<Weapon> {

    [SerializeField]
    private Text m_weaponNameText;

    public override void UpdateItem(Weapon Item, Action<Weapon> callback)
    {
        base.UpdateItem(Item, callback);
        m_weaponNameText.text = Item.N;
    }
}
