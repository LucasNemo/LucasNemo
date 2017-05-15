using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteBehaviour : MonoBehaviour {


    public void OnMuteClick()
    {
        if (AudioController.Instance.IsMusicMuted)
            AudioController.Instance.UnMuteMusic();
        else
            AudioController.Instance.MuteMusic();
    }

}
