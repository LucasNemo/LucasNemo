using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Renderer plane;

    private WebCamTexture camTexture;


    void Start()    
    {

        camTexture = new WebCamTexture();
        //camTexture.requestedHeight = 10;
        //camTexture.requestedWidth = 10;

        if (camTexture != null)
        {
            camTexture.Play();
        }
    }

    private void FixedUpdate()
    {
        plane.material.mainTexture = camTexture;
    }
}
