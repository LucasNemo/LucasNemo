using System.Collections;
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


        List<CharacterTip> characterTips = Manager.Instance.CharacterTips.Where(x => x.CT != m_corretHunch.HC.MC).ToList();
        List<WeaponTip> weaponsTips = Manager.Instance.WeaponsTips.Where(x => x.W != m_corretHunch.HR.W.MW).ToList();
        
        //Get all weapons
        List<Weapon> weapons = new List<Weapon>();
        weapons.AddRange(Manager.Instance.Weapons);
        
        foreach (var item in Manager.Instance.Places)
        //foreach (var item in m_placesBehaviour.GetPlaces)
        {
            var weapon = weapons.OrderBy(x => System.Guid.NewGuid()).FirstOrDefault();
            if (weapon != null)
            {
                m_gameInformation.Rs.Add(new Room(item, weapon));
                weapons.Remove(weapon);
            }
            else
                Debug.Log("Weapon is null");
        }
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
