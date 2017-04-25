using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARHud : MonoBehaviour {

    public GameObject notes;

	public void OnBackClicked()
    {
        SceneManager.LoadScene("DetectiveScene");
    }


    public void OnNotesClicked()
    {
        notes.SetActive(!notes.activeSelf);
    }
}
