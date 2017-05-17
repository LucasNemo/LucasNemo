using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTweenStart : MonoBehaviour {

    public EasyTween uiTween;

    public void Start()
    {
        uiTween.OpenCloseObjectAnimation();
    }
}
