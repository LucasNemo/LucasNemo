using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TipBehaviour : ARElement {


    public Enums.TipsSounds tipsSound;
    private bool m_disableAudio = false; 
    public void SetupTip(Enums.TipsSounds currentTipSound)
    {
        this.tipsSound = currentTipSound;

        onElementClicked = () =>
        {
            if (m_disableAudio) return;
            m_disableAudio = true; 
            var sound = LoadAudio(currentTipSound);
            AudioController.Instance.Play(sound, AudioController.SoundType.SoundEffect2D, 1f, false, true);
            Invoke("EnableAudio", sound.length);
        }; 
    }

    void EnableAudio()
    {
        m_disableAudio = false; 
    }

    public AudioClip LoadAudio(Enums.TipsSounds sound)
    {
        string name = "Audio/Tips/"+ sound.ToString();
        Debug.Log(name);
        return Resources.Load<AudioClip>(name); 
    }

}
