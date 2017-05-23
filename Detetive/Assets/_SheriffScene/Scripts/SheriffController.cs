using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

public class SheriffController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private PlaceBehaviour m_placesBehaviour;
    [SerializeField]
    private CharacterBehaviour m_setupGame;
    [SerializeField]
    private GameObject m_header, m_placesPanel, m_setupPanel;
    #endregion

    #region Private Att
    private GameInformation m_gameInformation;
    private Hunch m_corretHunch;
    private float m_timer = Manager.Instance.COOLDOWN;

    #endregion

    /// <summary>
    /// Get a game already set information
    /// </summary>
    public GameInformation GetGameInfo { get { return m_gameInformation; } }

    void Start()
    {
        m_placesBehaviour.gameObject.SetActive(true);
    }

    public void OnBackClick()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        SceneManager.LoadScene(Manager.Instance.MENU_SCENE);
    }

    /// <summary>
    /// When player finish add all places to play
    /// </summary>
    public void OnFinishAddPlaces()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        print("Finish AddPlaces");

        //todo - adicionar ao menos uma delegacia!!!!

        if (AnyPolicyStation())
        {
            InitializeGameInformation();
            m_placesBehaviour.DisablePlace();
            m_setupGame.gameObject.SetActive(true);
        }

        else
        {
            GenericModal.Instance.OpenAlertMode(Manager.Instance.NO_PD, "Ok", null);
        }
        //m_placesBehaviour.gameObject.SetActive(false);
    }

    private bool AnyPolicyStation()
    {
        var places = m_placesBehaviour.GetPlaces;

        //No place..no query is necessary
        if (places.Count == 0)
            return false;

        return places.Any(x => x.IH == 1);
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
    private void AddTip<T>(List<Place> places, List<T> tips, int minRandom) where T : TipItem
    {
        var randomPlaces = places.SortList();

        //Get random between all tips in game !         
        for (int i = 0; i < places.Count; i++)
        {
            var item = randomPlaces[i];

            var t = ReturnRandomItem<T>(tips);
            if (t == null || item == null) continue;

            var room = m_gameInformation.Rs.First(x => x.P.MP == item.MP);

            if (room == null) continue;
            if (room.T == null)
                room.T = new List<TipItem>();

            //Add a unique tip to each room!
            room.T.Add(new TipItem(t.TP));

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
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        GenericModal.Instance.OpenModal(Manager.Instance.PLAYER_WANNA_PLAY, "Não", "Sim", () =>
        {
            //If player dont wanna play
            SceneManager.LoadScene(Manager.Instance.MENU_SCENE);
        },
       () =>
       {
           m_setupGame.gameObject.SetActive(false);
           m_placesBehaviour.DisablePlace();
           m_header.SetActive(false);
           FindObjectOfType<ReadQRCodeBehaviour>().ReadQrCode((result) =>
           {
               SceneManager.UnloadScene(Manager.Instance.QRCODE_SCENE);

               if (result != Manager.Instance.RESULT_ERROR_BACK)
               {
                   int t = 0;
                   Character myCharacter = null;
                   if (int.TryParse(result, out t))
                       myCharacter = Manager.Instance.Characters.FirstOrDefault(x => x.MC == int.Parse(result));
                   if (myCharacter != null)
                   {
                       m_gameInformation.P = myCharacter;
                       Manager.Instance.MyGameInformation = m_gameInformation;
                       Manager.Instance.SaveGameInformation();
                       SceneManager.LoadScene(Manager.Instance.DETETIVE_SCENE);
                   }
                   else
                   {
                       GoBackSheriff();

                       GenericModal.Instance.OpenAlertMode(Manager.Instance.ON_READ_CHARACTER_WRONG, Manager.Instance.WARNING_BUTTON, null);
                   }
               }
               else
               {
                   GoBackSheriff();
               }
           }, false, Manager.Instance.READ_CHARACTER);
       });

        m_setupGame.gameObject.SetActive(false);
    }

    private void GoBackSheriff()
    {
        //TODO ADD ALERT
        m_setupGame.gameObject.SetActive(true);
        m_placesBehaviour.DisablePlace();
        m_header.SetActive(true);
    }

    public void OnBackHeaderButtonClick()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        SceneManager.LoadScene(Manager.Instance.MENU_SCENE);
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
        //    if (m_placesPanel.activeSelf)
        //    {
        //        OnBackHeaderButtonClick();
        //    }
           if (m_setupPanel.activeSelf)
            {
                m_setupPanel.SetActive(false);
                m_placesPanel.SetActive(true);
            }
        }
    }

}
