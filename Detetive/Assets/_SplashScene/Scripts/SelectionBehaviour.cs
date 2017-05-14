using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionBehaviour : MonoBehaviour
{
    public void OnBackClick()
    {
        FindObjectOfType<SplashController>().OnBackFromSelection();
    }
    
    public void OpenSettings()
    {
        FindObjectOfType<SplashController>().OpenSettings();
    }

    public void OnSheriffSelect()
    {
        SceneManager.LoadScene("SheriffScene");
    }

    public void OnDetectiveSelect()
    {
        SceneManager.LoadScene("DetectiveScene");
    }
    
}
