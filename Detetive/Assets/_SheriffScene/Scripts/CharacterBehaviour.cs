using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBehaviour : MonoBehaviour {

    [SerializeField]
    private CharacterSelectionItem CharacterGridItem;
    [SerializeField]
    private GridLayoutGroup m_grid;
    [SerializeField]
    private Image m_qrcodeImage;

    private GameInformation m_gameInformation;

    void Start () {
        AddCharacterListGrid();
        m_gameInformation = FindObjectOfType<SheriffController>().GetGameInfo;
    }

    private void AddCharacterListGrid()
    {
        foreach (Character character in Manager.Instance.Characters)
        {
            var selection = GameObject.Instantiate<CharacterSelectionItem>(CharacterGridItem, m_grid.transform);
            selection.UpdateItem(character, OnCharacterClick);
        }
    }

    private void OnCharacterClick(Character character)
    {
        m_gameInformation.P = character;
        var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(m_gameInformation);
        //print(serialized);
        Sprite qrCode = GenerateQRCode.GenerateQRSprite(serialized, 256);
        m_qrcodeImage.sprite = qrCode;
    }

}
