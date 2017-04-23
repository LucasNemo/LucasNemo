using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour {

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(1);
    }

    public void OnGUI()
    {

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 200), "Testar QRCodes"))
            {
                SceneManager.LoadScene(2);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 150, 200, 200), "Testar AR"))
            {
                SceneManager.LoadScene(3);
            }
        }
        else 
        {
            if ( GUI.Button(new Rect(0,Screen.height - 200,200,200), "MENU"))
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }


}
