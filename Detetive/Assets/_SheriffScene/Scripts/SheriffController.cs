using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;
 
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
    private float m_timer = Manager.Instance.COOLDOWN;

    [SerializeField]
    private WannaPlayBehaviour m_wannaPlay;

    #endregion

    #region Properties

    public float Timer { get { return m_timer; } }
    #endregion 

    public void OnSelectWannaPlay(bool wannaPlay)
    {
        m_wannaPlay.gameObject.SetActive(false);
        m_characterBehaviour.gameObject.SetActive(true);

        //Start the timer to start the game! 
        StartCoroutine(StartTimerController());
    }

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
        m_wannaPlay.gameObject.SetActive(true);
    }

    private IEnumerator StartTimerController()
    {
        do
        {
            m_timer -= Time.deltaTime;
            m_timer = (float) Math.Round(m_timer, 2);
            yield return null;
        } while (m_timer > 0);

        m_timer = 0;
    }

    /// <summary>
    /// Initialize game information add rooms and weapon on each of it
    /// </summary>
    private void InitializeGameInformation()
    {
        UpdateManagerPlaces();

        //Make correct hunch
        m_corretHunch = new Hunch(new Room(Manager.Instance.Places[Helper.Random(0, Manager.Instance.Places.Count)],
                                           Manager.Instance.Weapons[Helper.Random(0, Manager.Instance.Weapons.Count)]),
                                           Manager.Instance.Characters[Helper.Random(0, Manager.Instance.Characters.Count)].MC);


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
        List<WeaponTip> weaponsTips = Manager.Instance.WeaponsTips.Where(x => x.W.GetHashCode() != m_corretHunch.HR.W.MW).ToList();
        AddTip(places, weaponsTips, Manager.Instance.Min_Weapons_Tips);

        //Add character tips
        List<CharacterTip> characterTips = Manager.Instance.CharacterTips.Where(x => x.CT.GetHashCode() != m_corretHunch.HC).ToList();
        AddTip(places, characterTips, Manager.Instance.Min_Character_Tips);
    }

    private void UpdateManagerPlaces()
    {
        var places = m_placesBehaviour.GetPlaces;

        foreach (var item in places)
        {
            foreach (var mPlaces in Manager.Instance.Places)
            {
                if (mPlaces.MP == item.MP)
                {
                    mPlaces.N = item.N;
                    mPlaces.IH = item.IH;
                    break;
                }
            }
        }
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
        var randomPlaces = places.SortList();

        //Get random between all tips in game !         
        var rand = UnityEngine.Random.Range(minRandom, tips.Count);

        for (int i = 0; i < places.Count; i++)
        {
            var item = randomPlaces[i];

            //Add a unique tip to each room!
            m_gameInformation.Rs.First(x => x.P.MP == item.MP).T.Add(new TipItem(ReturnRandomItem<T>(tips).TP));

            if (tips.Count <= 0) return;
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
        //List<Weapon> weapons = new List<Weapon>();
        //weapons.AddRange(Manager.Instance.Weapons);

        var randomPlaces = places.SortList();
        var item = randomPlaces[0];
        m_gameInformation.Rs.First(x => x.P.MP == item.MP).W = weapon;

        //for (int i = 1; i < UnityEngine.Random.Range(Manager.Instance.Min_Weapons, weapons.Count); i++)
        //{
        //    item = randomPlaces[i];
        //    m_gameInformation.Rs.First(x => x.P.MP == item.MP).W = ReturnRandomItem(weapons);
        //}
    }

    /// <summary>
    /// Return a random generic item then remove from list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    private T ReturnRandomItem<T>(List<T> list)
    {
        var listItem = list.SortList().FirstOrDefault();
        if (listItem != null)
        {
            list.Remove(listItem);
        }
        return listItem;
    }
  
   
    /// <summary>
    /// Method called when player finish add detectives
    /// </summary>
    public void OnFinishAddCharacter()
    {
        if (Manager.Instance.SheriffWannaPlay)
        {
            SceneManager.LoadScene("DetectiveScene");
        }
        else
        {
            //TODO O QUE ROLA QUANDO DELE NÃO QUER JOGAR?!@#!$!@
        }

        m_characterBehaviour.gameObject.SetActive(false);
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
