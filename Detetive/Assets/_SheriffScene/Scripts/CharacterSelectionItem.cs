using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionItem : MonoBehaviour {
    
    [SerializeField]
    private Image m_characterImage;
    [SerializeField]
    private Text m_characterText;
    private Character m_character;

    //Callback to return clicked item
    private System.Action<Character> m_clickCallback;

    /// <summary>
    /// Add a character grid informations
    /// </summary>
    /// <param name="character"></param>
    /// <param name="callback"></param>
    public void UpdateItem(Character character, System.Action<Character> callback)
    {
        m_characterText.text = character.Name;
        m_clickCallback = callback;
        m_character = character;
    }

    public void OnClick()
    {
        if (m_clickCallback != null)
            m_clickCallback(m_character);
    }

}
