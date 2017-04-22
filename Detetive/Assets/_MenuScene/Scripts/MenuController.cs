using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    
	void Start () {
		
	}

    /// <summary>
    /// 0 is Detective
    /// 1 is Sheriff
    /// </summary>
    /// <param name="type"></param>
    public void SelectGameType(int type)
    {
        if (type == 0)
            SceneManager.LoadScene("DetectiveScene");
        else
            SceneManager.LoadScene("SheriffScene");
    }
}
