using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<SpriteSelectionBehaviour> m_selectionsButtons;

    private void Start()
    {
        SelectItem(0);
    }

    public void OnBackClick()
    {
        FindObjectOfType<SplashController>().OnBackFromSelection();
    }

    public void SelectItem(int index)
    {
        m_selectionsButtons.ForEach(x => x.DeselectSprite());
        m_selectionsButtons[index].SelectSprite();
    }

    public void OpenSettings()
    {
        FindObjectOfType<SplashController>().OpenSettings();
    }

    public void OnPlayClick()
    {
        int selected = m_selectionsButtons.FindIndex(x => x.IsSelected);

        switch (selected)
        {
            case -1:
            break;
            case 0:
                SceneManager.LoadScene("SheriffScene");
                break;
            case 1:
                SceneManager.LoadScene("DetectiveScene");
                break;
        }

    }

}
