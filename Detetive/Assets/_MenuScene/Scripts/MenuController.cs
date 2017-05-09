using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private GameObject m_newGameMenu, m_continueMenu;
    
	void Start () {
        if(Manager.Instance.isOnGame)
        {
            m_continueMenu.SetActive(true);
        }
        else
        {
            m_newGameMenu.SetActive(true);
        }
	}

    /// <summary>
    /// 0 is Detective
    /// 1 is Sheriff
    /// 2 is continue game
    /// 3 is on begin new game
    /// </summary>
    /// <param name="type"></param>
    public void SelectGameType(int type)
    {
        switch (type)
        {
            case 0:
                SceneManager.LoadScene("DetectiveScene");
                break;
            case 1:
                SceneManager.LoadScene("SheriffScene");
                break;
            case 2:
                SceneManager.LoadScene("DetectiveScene");
                break;
            case 3:
                Manager.Instance.MyGameInformation = null;
                m_continueMenu.SetActive(false);
                m_newGameMenu.SetActive(true);
                break;
        }
    }
}
