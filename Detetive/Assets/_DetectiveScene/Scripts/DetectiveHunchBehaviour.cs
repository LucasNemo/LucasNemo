using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DetectiveHunchBehaviour : MonoBehaviour {
    
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

    void Awake()
    {
        InitializeGrid(m_gridCharacters, m_characterItem, Manager.Instance.Characters, OnCharacterCallback, out characters);
        InitializeGrid(m_gridPlaces, m_placeItem, Manager.Instance.Places, OnPlaceCallback, out places);
        InitializeGrid(m_gridWeapons, m_weaponItem, Manager.Instance.Weapons, OnWeaponCallback,out weapons);
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

        weapons.ForEach(x => x.UnSelectItem());

        m_selectedWeapon = weapon;

        weapons.Where(x => x.GetItem.MW == m_selectedWeapon.MW).First().SelectItem();

    }

    private void OnPlaceCallback(Place place)
    {
        places.ForEach(x => x.UnSelectItem());

        m_selectedPlace = place;

        places.Where(x => x.GetItem.MP == m_selectedPlace.MP).First().SelectItem();
    }

    private void OnCharacterCallback(Character character)
    {
        characters.ForEach(x => x.UnSelectItem());

        m_selectedCharacter = character;

        characters.Where(x => x.GetItem.MC == m_selectedCharacter.MC).First().SelectItem();

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
}
