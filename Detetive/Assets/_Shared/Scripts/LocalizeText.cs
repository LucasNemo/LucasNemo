using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizeText : MonoBehaviour {

    public string Key;

    public void Awake()
    {
        GetComponent<Text>().text = LocalizationManager.instance.GetText(Key);
    }
}
