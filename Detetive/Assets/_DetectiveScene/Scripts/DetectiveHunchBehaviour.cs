using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DetectiveHunchBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject m_panelCharacters, m_panelWeapons, m_panelPlaces;
    [SerializeField]
    private ConfirmHunchBehaviour m_confirmHunch;
    [SerializeField]
    private GridLayoutGroup m_gridCharacters, m_gridPlaces, m_gridWeapons;
    [SerializeField]
    private CharacterSelectionItem m_characterItem;
    [SerializeField]
    private PlaceSelectionItem m_placeItem;
    [SerializeField]
    private WeaponSelectionItem m_weaponItem;

    private Weapon m_selectedWeapon;
    private Place m_selectedPlace;
    private Character m_selectedCharacter;
    private PlayerHunch m_hunch;

    public PlayerHunch GetHunch { get { return m_hunch; } }
    
    private List<CharacterSelectionItem> characters;
    private List<PlaceSelectionItem> places;
    private List<WeaponSelectionItem> weapons;
    private DetectiveController m_detectiveController;
    private Sprite[] cards;

    void Awake()
    {
        cards = Resources.LoadAll<Sprite>(string.Format(Manager.Instance.HUNCH_PATH, "AF_Detetive_Cartas_FT"));

        InitializeGrid(m_gridCharacters, m_characterItem, Manager.Instance.Characters, OnCharacterCallback, out characters);

        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].UpdateSprite(cards[i]);
        }


        InitializeGrid(m_gridWeapons, m_weaponItem, Manager.Instance.Weapons, OnWeaponCallback, out weapons);
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].UpdateSprite(cards[characters.Count + i]);
        }
        
        InitializeGrid(m_gridPlaces, m_placeItem, Manager.Instance.Places, OnPlaceCallback, out places);

        for (int i = 0; i < places.Count; i++)
        {
            places[i].UpdateSprite( cards[characters.Count + weapons.Count + i]);
        }
    }

    private void Start()
    {
        m_detectiveController = FindObjectOfType<DetectiveController>();
    }

    /// <summary>
    /// do some amazing comments =X
    /// </summary>
    /// <typeparam name="T">The GenericSelectedItem</typeparam>
    /// <typeparam name="G">the correspondent model - places, characters, whatever!! =)</typeparam>
    /// <param name="grid"></param>
    /// <param name="itemPrefab"></param>
    /// <param name="itensList"></param>
    /// <param name="callback"></param>
    /// <param name="myList"></param>
    private void InitializeGrid<T, G>(GridLayoutGroup grid, T itemPrefab, List<G> itensList, Action<G> callback, out List<T> myList)
        where T : GenericSelectItem<G>
    {
        myList = new List<T>();

        foreach (G item in itensList)
        {
            var selection = Instantiate(itemPrefab, grid.transform);
            selection.UpdateItem(item, callback);
            myList.Add(selection);
        }
    }

    private void OnWeaponCallback(Weapon weapon)
    {
        m_selectedWeapon = weapon;
        m_panelWeapons.SetActive(false);
        m_panelPlaces.SetActive(true);
    }

    private void OnPlaceCallback(Place place)
    {
        m_selectedPlace = place;
        m_panelPlaces.SetActive(false);
        m_confirmHunch.gameObject.SetActive(true);

        Sprite weaponSprite = cards.First(x => x.name == GetCorrectCard(m_selectedWeapon.MW));
        Sprite characterSprite = cards.First(x => x.name == GetCorrectCard(m_selectedCharacter.MC));
        Sprite placeSprite = cards.First(x => x.name == GetCorrectCard(m_selectedPlace.MP));

        m_confirmHunch.UpdateInformation(characterSprite, weaponSprite, placeSprite, formatResult(), () => {
            m_confirmHunch.gameObject.SetActive(false);
            m_panelCharacters.SetActive(true);
        }, 
        () => {
            m_confirmHunch.gameObject.SetActive(false);
            m_detectiveController.OnFinishHunchClicked();
        });
    }

    private string GetCorrectCard(int card)
    {
        if (card < 10)
            return "0" + card.ToString();

        return card.ToString();
    }

    private string formatResult()
    {
        string character = Manager.Instance.CharactersName[ (Enums.Characters) m_selectedCharacter.MC ];
        string weaponArtigo = GetArtigoWeapon((Enums.Weapons)m_selectedWeapon.MW);
        string weapon = Manager.Instance.WeaponsName[(Enums.Weapons)m_selectedWeapon.MW ];
        string placeArtigo = "";
        string place = m_selectedPlace.N;

        return string.Format(Manager.Instance.FINAL_CONFIRM_HUNCH, character, weaponArtigo, weapon, placeArtigo, place);
    }

    private string GetArtigoWeapon(Enums.Weapons weapon)
    {
        switch (weapon)
        {
            case Enums.Weapons.Pa:
            case Enums.Weapons.Faca:
            case Enums.Weapons.Espingarda:
            case Enums.Weapons.Tesoura:
            case Enums.Weapons.Arma_Quimica:
                return "A";
            case Enums.Weapons.Pe_de_Cabra:
            case Enums.Weapons.Soco_Ingles:
            case Enums.Weapons.Veneno:
                return "O";
        }

        return "A";
    }

    //private string GetPlaceArtigo(Enums.Places place)
    //{
    //    switch (place)
    //    {
    //        case Enums.Places.Cemiterio:
    //        case Enums.Places.Banco:
    //        case Enums.Places.Hospital:
    //        case Enums.Places.Hotel:
    //        case Enums.Places.Restaurante:
    //            return "O";
    //        case Enums.Places.Boate:
    //        case Enums.Places.Estacao_de_Trem:
    //        case Enums.Places.Floricultura:
    //        case Enums.Places.Mansao:
    //        case Enums.Places.Praca_Central:
    //        case Enums.Places.Prefeitura:
    //            return "A";
    //    }
    //   return "A";
    //}

    private void OnCharacterCallback(Character character)
    {
        m_selectedCharacter = character;
        m_panelCharacters.SetActive(false);
        m_panelWeapons.SetActive(true);
    }

    /// <summary>
    /// On finish select a hunch
    /// </summary>
    public void OnFinishHunch()
    {
        if(m_selectedCharacter == null)
        {
            print("Necessario escolher um personagem");
            return;
        }

        if (m_selectedPlace == null)
        {
            print("Necessario escolher um lugar");
            return;
        }

        if (m_selectedWeapon == null)
        {
            print("Necessario escolher uma arma");
            return;
        }
        
        m_hunch = new PlayerHunch(Manager.Instance.MyGameInformation.P, 
            new Hunch(new Room(m_selectedPlace, m_selectedWeapon), m_selectedCharacter.MC), DateTime.Now.Ticks);
    }

    public void OnBackFromCharacter()
    {
        m_detectiveController.OnBackFromHunch();
    }

    public void OnBackFromWeapon()
    {
        m_panelWeapons.SetActive(false);
        m_panelCharacters.SetActive(true);
    }

    public void OnBackFromPlace()
    {
        m_panelPlaces.SetActive(false);
        m_panelWeapons.SetActive(true);
    }
}
