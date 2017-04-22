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

}
