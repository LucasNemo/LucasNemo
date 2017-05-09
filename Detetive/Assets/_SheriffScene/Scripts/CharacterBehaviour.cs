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

    private SheriffController m_sheriffController;

    void Start () {
        AddCharacterListGrid();
        m_sheriffController = FindObjectOfType<SheriffController>();
        m_gameInformation = m_sheriffController.GetGameInfo;
    }

    private void AddCharacterListGrid()
    {
        foreach (Character character in Manager.Instance.Characters)
        {
            var selection = Instantiate<CharacterSelectionItem>(CharacterGridItem, m_grid.transform);
            selection.UpdateItem(character, OnCharacterClick);
        }
    }

    private void OnCharacterClick(Character character)
    {
        m_gameInformation.P = character;
        float timer = m_sheriffController.Timer - 5f > 0 ? m_sheriffController.Timer - 5f : 0;
        m_gameInformation.Timer = timer;
        var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(m_gameInformation);
        print(serialized);
        Sprite qrCode = GenerateQRCode.GenerateQRSprite(serialized, 256);
        m_qrcodeImage.sprite = qrCode;
    }

}
