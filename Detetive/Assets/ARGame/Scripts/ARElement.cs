using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARElement : MonoBehaviour {

    public enum ARType
    {
        Weapon,
        Tip
    }

    public ARType arType;

    [Tooltip("Not required!")]
    public Enums.Weapons weaponType;

    public Action onElementClicked;

    Material mainMaterial;

    private void Start() 
    {
        mainMaterial = GetComponent<Renderer>().material;        
    }

    private void OnMouseDown()
    {

        if (onElementClicked != null)
        {
            onElementClicked.Invoke();
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
