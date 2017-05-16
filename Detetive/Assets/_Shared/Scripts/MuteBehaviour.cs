using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteBehaviour : MonoBehaviour {
    
    public GameObject musicOn, musicOff;

    public void Start()
    {
        SwitchStates();

    }

    private void SwitchStates()
    {
        var musicMuted = AudioController.Instance.IsMusicMuted;
        musicOn.SetActive(!musicMuted);
        musicOff.SetActive(musicMuted);
    }

    public void OnMuteClick()
    {
        if (AudioController.Instance.IsMusicMuted)
            AudioController.Instance.UnMuteMusic();
        else
            AudioController.Instance.MuteMusic();

        SwitchStates();
    }

}
