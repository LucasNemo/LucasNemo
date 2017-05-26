using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DetectiveHunchBehaviour : MonoBehaviour, IBackButton {

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
        //string.Format(Manager.Instance.HUNCH_PATH, "AF_Detetive_Cartas_FT")
        cards = Resources.LoadAll<Sprite>(Manager.Instance.HUNCH_PATH);

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
            var selection = Instantiate(itemPrefab, grid.transform, false);
            selection.UpdateItem(item, callback);
            myList.Add(selection);
        }
    }

    private void OnWeaponCallback(Weapon weapon)
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_selectedWeapon = weapon;
        m_panelWeapons.SetActive(false);
        m_panelPlaces.SetActive(true);
    }

    private void OnPlaceCallback(Place place)
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_selectedPlace = place;
        m_panelPlaces.SetActive(false);
        m_confirmHunch.gameObject.SetActive(true);

        Sprite weaponSprite = Manager.Instance.ReturnCorrectImage(cards, m_selectedWeapon.MW);
        Sprite characterSprite = Manager.Instance.ReturnCorrectImage(cards, m_selectedCharacter.MC); 
        Sprite placeSprite = Manager.Instance.ReturnCorrectImage(cards, m_selectedPlace.MP);

        m_confirmHunch.UpdateInformation(characterSprite, weaponSprite, placeSprite, Manager.Instance.FormatResult(m_selectedCharacter, m_selectedWeapon, m_selectedPlace), () =>
        {
            OnBackFromConfirmHunch();
        }, 
        () => {
            m_confirmHunch.gameObject.SetActive(false);
            m_detectiveController.OnFinishHunchClicked();
        });
    }

    private void OnBackFromConfirmHunch()
    {
        m_confirmHunch.gameObject.SetActive(false);
        m_panelCharacters.SetActive(true);
    }
    
    public void ActiveConfirmModal(bool active)
    {
        m_confirmHunch.gameObject.SetActive(active);
    }
    
    private void OnCharacterCallback(Character character)
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_selectedCharacter = character;
        m_panelCharacters.SetActive(false);
        m_panelWeapons.SetActive(true);
    }

    /// <summary>
    /// On finish select a hunch
    /// </summary>
    public void OnFinishHunch()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        if (m_selectedCharacter == null)
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
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_detectiveController.OnBackFromHunch();
    }

    public void OnBackFromWeapon()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_panelWeapons.SetActive(false);
        m_panelCharacters.SetActive(true);
    }

    public void OnBackFromPlace()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_panelPlaces.SetActive(false);
        m_panelWeapons.SetActive(true);
    }

    private void OnEnable()
    {
        BackButtonManager.Instance.AddBackButton(this);
    }

    private void OnDisable()
    {
        BackButtonManager.Instance.RemoveBackButton(this);
    }

    public void OnAndroidBackButtonClick()
    {
        if (m_panelCharacters.activeSelf) OnBackFromCharacter();
        else if (m_panelWeapons.activeSelf) OnBackFromWeapon();
        else if (m_panelPlaces.activeSelf) OnBackFromPlace();
        else if (m_confirmHunch.isActiveAndEnabled) OnBackFromConfirmHunch();
    }
}
