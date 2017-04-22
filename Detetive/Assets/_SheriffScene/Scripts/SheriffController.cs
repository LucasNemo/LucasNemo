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
    
    public GameInformation GetGameInfo { get { return m_gameInformation; } }

    void Start () {
        m_placesBehaviour.gameObject.SetActive(true);
    }
    
    public void OnBackClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

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
        m_gameInformation = new GameInformation();

        List<Weapon> weapons = new List<Weapon>();
        weapons.AddRange(Manager.Instance.Weapons);

        //foreach (var item in m_placesBehaviour.GetPlaces)
        foreach (var item in Manager.Instance.Places)
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

        m_corretHunch = new Hunch(m_gameInformation.Rs[0], Manager.Instance.Characters[0], 0);
        print("Character " + m_corretHunch.HC.N + " Place " + m_corretHunch.HR.P.N + " Weapon " + m_corretHunch.HR.W.N);
    }

    public void OnFinishAddCharacter()
    {
        m_characterBehaviour.gameObject.SetActive(false);
        m_hunchBehaviour.gameObject.SetActive(true);
    }

    public void OnFinishAddHunch()
    {
        m_hunchBehaviour.gameObject.SetActive(false);
        m_finishBehaviour.gameObject.SetActive(true);
    }

    public PlayerHunch GetBestHunch()
    {
        return m_hunchBehaviour.GetHunchs.Where(x => x.MH == m_corretHunch).OrderBy(y => y.MH.HT).FirstOrDefault(); 
    }

}
