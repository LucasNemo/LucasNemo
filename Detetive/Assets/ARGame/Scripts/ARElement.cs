using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARElement : MonoBehaviour {

    Material mainMaterial;

    private void Start() 
    {
        mainMaterial = GetComponent<Renderer>().material;        
    }

    private void OnMouseEnter()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mainMaterial.color = Color.red;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mainMaterial.color = Color.white;
        }

        transform.Rotate(new Vector3(0, 0, Time.deltaTime * 20f ));
    }

    private void OnMouseExit()
    {
    }
}
