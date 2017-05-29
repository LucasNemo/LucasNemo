using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour {

    [SerializeField]
    private GameObject m_intro, m_selection, m_settings;

    public AudioClip menu;

    private void Start()
    {
        AudioController.Instance.Play(menu, AudioController.SoundType.Music);
    }
    
    public void OpenSelection()
    {
        m_intro.SetActive(false);
        m_selection.SetActive(false);
        m_selection.SetActive(true);


        var selection = PlayerPrefs.GetInt("Select", 0);
        if (selection == 0)
        {
            TutorialController.Show("Vamos lá! Sua primeira missão: Escolha seu papel no game! Você pode optar por ser Xerife ou Detetive. Xerife é a pessoal que configura o jogo e pode saber a resposta (se desejar) ou também poderá jogar. Detetive são os jogares que irão desvendar o assassinato.", TutorialController.TutorialStage.Perfil);
        }
    }

    public void OpenSettings()
    {
        m_selection.SetActive(false);
        m_settings.SetActive(true);
    }
    
    public void OnBackFromSelection()
    {
        m_selection.SetActive(false);
        m_intro.SetActive(true);
    }

    public void OnBackFromSettings()
    {
        m_settings.SetActive(false);
        m_selection.SetActive(true);
    }

}
