using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TipBehaviour : ARElement {


    public Enums.TipsSounds tipsSound;
    private bool m_disableAudio = false;
    private bool m_previousMusicState;

    public void SetupTip(Enums.TipsSounds currentTipSound)
    {
        this.tipsSound = currentTipSound;

        onElementClicked = () =>
        {
            if (m_disableAudio) return;
            m_disableAudio = true;

            m_previousMusicState = AudioController.Instance.IsMusicMuted;

            AudioController.Instance.MuteMusic();

            var sound = LoadAudio(currentTipSound);

            AudioController.Instance.Play(sound, AudioController.SoundType.SoundEffect2D, 1f, false, true);

            //Enable tap again this audio and enable the music
            Invoke("EnableAudio", sound.length);
        }; 
    }

    void EnableAudio()
    {
        m_disableAudio = false;
        if (!m_previousMusicState)
            AudioController.Instance.UnMuteMusic();
    }

    public AudioClip LoadAudio(Enums.TipsSounds sound)
    {
        string name = "Audio/Tips/"+ sound.ToString();
        Debug.Log(name);
        return Resources.Load<AudioClip>(name); 
    }

}
