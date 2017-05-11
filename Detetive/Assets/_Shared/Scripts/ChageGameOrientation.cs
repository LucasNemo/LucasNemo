using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChageGameOrientation : MonoBehaviour
{
    public ScreenOrientation newOrientation;
    public ScreenOrientation orientationWhenLeave;
	void Awake ()
    {
        Screen.orientation = newOrientation;
	}

    private void OnDestroy()
    {
        Screen.orientation = orientationWhenLeave;
    }
}
