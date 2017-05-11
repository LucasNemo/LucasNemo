using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public Image plane;
    private WebCamTexture camTexture;

    void Start()    
    {
        camTexture = new WebCamTexture();
        if (camTexture != null)
            camTexture.Play();

        plane.material.mainTexture = camTexture;

        Material m = new Material(plane.material);
        m.mainTexture = camTexture;
        plane.material = m;

    }

    private void FixedUpdate()
    {
        plane.transform.localRotation = Quaternion.Euler(0, 0, camTexture.videoRotationAngle);
    }

    private void OnDestroy()
    {
        if (camTexture!= null)
            camTexture.Stop();
    }
}
