using System;
using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        InitializeGrid<CharacterSelectionItem, Character>(m_gridCharacters, m_characterItem, Manager.Instance.Characters, OnCharacterCallback);
        InitializeGrid<PlaceSelectionItem, Place>(m_gridPlaces, m_placeItem, Manager.Instance.Places, OnPlaceCallback);
        InitializeGrid<WeaponSelectionItem, Weapon>(m_gridWeapons, m_weaponItem, Manager.Instance.Weapons, OnWeaponCallback);
    }

    private void InitializeGrid<T, G>(GridLayoutGroup grid, T itemPrefab, List<G> itensList, Action<G> callback) where T : GenericSelectItem<G>
    {
        foreach (G item in itensList)
        {
            var selection = GameObject.Instantiate<T>(itemPrefab, grid.transform);
            selection.UpdateItem(item, callback);
        }
    }

    private void OnWeaponCallback(Weapon weapon)
    {
        m_selectedWeapon = weapon;
    }

    private void OnPlaceCallback(Place place)
    {
        m_selectedPlace = place;
    }

    private void OnCharacterCallback(Character character)
    {
        m_selectedCharacter = character;
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
            new Hunch(new Room(m_selectedPlace, m_selectedWeapon), m_selectedCharacter, DateTime.Now.Ticks));
    }
}
