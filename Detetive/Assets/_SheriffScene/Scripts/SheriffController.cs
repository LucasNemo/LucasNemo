﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class SheriffController : MonoBehaviour {

    #region Editor Variables
    [SerializeField]
    private PlaceBehaviour m_placesBehaviour;
    [SerializeField]
    private CharacterBehaviour m_characterBehaviour;
    [SerializeField]
    private HunchBehaviour m_hunchBehaviour;
    [SerializeField]
    private FinishGameBehaviour m_finishBehaviour;
    #endregion

    #region Private Att
    private GameInformation m_gameInformation;
    private Hunch m_corretHunch;
    #endregion
    
    /// <summary>
    /// Get a game already set information
    /// </summary>
    public GameInformation GetGameInfo { get { return m_gameInformation; } }

    void Start () {
        m_placesBehaviour.gameObject.SetActive(true);
    }
    
    public void OnBackClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    /// <summary>
    /// When player finish add all places to play
    /// </summary>
    public void OnFinishAddPlaces()
    {
        InitializeGameInformation();
        m_placesBehaviour.gameObject.SetActive(false);
        m_characterBehaviour.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// Initialize game information add rooms and weapon on each of it
    /// </summary>
    private void InitializeGameInformation()
    {
        //Make correct hunch
        m_corretHunch = new Hunch(new Room(Manager.Instance.Places[Helper.Random(0, Manager.Instance.Places.Count)],
                                           Manager.Instance.Weapons[Helper.Random(0, Manager.Instance.Weapons.Count)]),
                                           Manager.Instance.Characters[Helper.Random(0, Manager.Instance.Characters.Count)]);


        m_gameInformation = new GameInformation(m_corretHunch);
        
        List<Place> places = new List<Place>();
        //places.AddRange(m_placesBehaviour.GetPlaces);
        places.AddRange(Manager.Instance.Places);

        //Initialize evert room
        foreach (Place place in places)
        {
            m_gameInformation.Rs.Add(new Room(place));
        }

        //Add weapons on rooms
        AddWeapons(places, m_corretHunch.HR.W);

        //AddWeaponsTips
        List<WeaponTip> weaponsTips = Manager.Instance.WeaponsTips.Where(x => x.W != m_corretHunch.HR.W.MW).ToList();
        AddTip(places, weaponsTips, Manager.Instance.Min_Weapons_Tips);

        //Add character tips
        List<CharacterTip> characterTips = Manager.Instance.CharacterTips.Where(x => x.CT != m_corretHunch.HC.MC).ToList();
        AddTip(places, characterTips, Manager.Instance.Min_Character_Tips);
    }
    
    /// <summary>
    /// Generic add tip method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="places"></param>
    /// <param name="tips"></param>
    /// <param name="minRandom"></param>
    private void AddTip<T>(List<Place> places,List<T> tips, int minRandom) where T : TipItem
    {
        var randomPlaces = GetRandomPlaces(places);

        for (int i = 0; i < Random.Range(Manager.Instance.Min_Character_Tips, tips.Count); i++)
        {
            var item = randomPlaces[i];
            m_gameInformation.Rs.First(x => x.P.MP == item.MP).T.Add(new TipItem(ReturnRandomItem<T>(tips).TP));
        }
    }

    /// <summary>
    /// Add weapons on game
    /// </summary>
    /// <param name="places"></param>
    /// <param name="Weapon"> The GAME weapon</param>
    private void AddWeapons(List<Place> places, Weapon weapon)
    {
        //Get all weapons
        List<Weapon> weapons = new List<Weapon>();
        weapons.AddRange(Manager.Instance.Weapons);

        var randomPlaces = GetRandomPlaces(places);

        var item = randomPlaces[0];
        m_gameInformation.Rs.First(x => x.P.MP == item.MP).W = weapon;

        for (int i = 1; i < Random.Range(Manager.Instance.Min_Weapons, weapons.Count); i++)
        {
            item = randomPlaces[i];
            m_gameInformation.Rs.First(x => x.P.MP == item.MP).W = ReturnRandomItem(weapons);
        }
    }

    /// <summary>
    /// Return a random generic item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    private T ReturnRandomItem<T>(List<T> list)
    {
        var listItem = list.OrderBy(x => System.Guid.NewGuid()).FirstOrDefault();
        if (listItem != null)
        {
            list.Remove(listItem);
        }
        return listItem;
    }
    
    /// <summary>
    /// Random places
    /// </summary>
    /// <param name="places"></param>
    /// <returns></returns>
    private List<Place> GetRandomPlaces(List<Place> places)
    {
        return places.OrderBy(x => System.Guid.NewGuid()).ToList();
    }
   
    /// <summary>
    /// Method called when player finish add detectives
    /// </summary>
    public void OnFinishAddCharacter()
    {
        m_characterBehaviour.gameObject.SetActive(false);
        m_hunchBehaviour.gameObject.SetActive(true);
    }

    /// <summary>
    /// Method called when sheriff finish add hunch 
    /// </summary>
    public void OnFinishAddHunch()
    {
        m_hunchBehaviour.gameObject.SetActive(false);
        m_finishBehaviour.gameObject.SetActive(true);
    }

    public PlayerHunch GetBestHunch()
    {
        return m_hunchBehaviour.GetHunchs.Where(x => x.MH == m_corretHunch).OrderBy(y => y.HT).FirstOrDefault(); 
    }

}
