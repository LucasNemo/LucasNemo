using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {
    
	public void OnChooseClick()
    {
        FindObjectOfType<SplashController>().OpenSelection();
    }

    public void OnSettingsClick()
    {
        //TODO open settings here
    }

}
