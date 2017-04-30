using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionItem : GenericSelectItem<Character> {
    
    [SerializeField]
    private Image m_characterImage;
    [SerializeField]
    private Text m_characterText;

    public override void UpdateItem(Character Item, Action<Character> callback)
    {
        base.UpdateItem(Item, callback);
        m_characterText.text = Manager.Instance.CharactersName[ (Enums.Characters) Item.MC];
    }
}
