using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultBehaviour : MonoBehaviour {

    [SerializeField]
    private Text m_title, m_description;
	
    public void UpdateInformation(bool win)
    {
        m_title.text = win ? Manager.Instance.WIN_TITLE : Manager.Instance.LOOSE_TITLE;
        m_description.text = win ? Manager.Instance.WIN_DESCRIPTION : Manager.Instance.LOOSE_DESCRIPTION;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Splash");
    }

}
