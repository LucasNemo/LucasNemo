using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceSelectionItem : GenericSelectItem<Place> {

    [SerializeField]
    private Text m_nameText;

    public override void UpdateItem(Place Item, Action<Place> callback)
    {
        base.UpdateItem(Item, callback);
        m_nameText.text = Item.N;
    }

    public override void SelectItem()
    {
        m_nameText.color = Color.red;

        base.SelectItem();
    }

    public override void UnSelectItem()
    {
        base.UnSelectItem();
        m_nameText.color = Color.black;
    }

}
