using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARHud : MonoBehaviour {

    public GameObject notes;

    public DeviceCameraController deviceCamera;

	public void OnBackClicked()
    {
        deviceCamera.StopWork();

        SceneManager.LoadScene("DetectiveScene");
    }
  
}
