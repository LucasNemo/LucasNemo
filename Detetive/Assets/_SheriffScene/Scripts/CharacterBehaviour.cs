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
        m_gameInformation.Player = character;
        var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(m_gameInformation);
        print(serialized);
        Texture2D qrCode = GenerateQRCode.GenerateQR(serialized, 256);
        m_qrcodeImage.sprite = Sprite.Create(qrCode, new Rect(0, 0, qrCode.width, qrCode.height), new Vector2(0.5f, 0.5f));
    }

}
