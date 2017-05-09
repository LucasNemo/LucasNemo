using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour {

    GenerateQRCode qrcode; 

    private void Start()
    {
        // DontDestroyOnLoad(gameObject);
        //   SceneManager.LoadScene(1);

        qrcode = new GenerateQRCode();
    }
    string txt = "40";
    Texture t;
    public void OnGUI()
    {

        txt = GUI.TextField(new Rect(0, 100, 100, 50), txt);

        if (GUI.Button(new Rect(0,0,100,50), "x"))
        {
          
            t = GenerateQRCode.GenerateQR(txt, 256, false);
        }

        if(t)
           GUI.DrawTexture(new Rect(0, 200, 512, 512), t);
    }


}
