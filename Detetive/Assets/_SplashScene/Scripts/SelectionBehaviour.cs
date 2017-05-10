using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBehaviour : MonoBehaviour {

    [SerializeField]
    private List<SpriteSelectionBehaviour> m_selectionsButtons;
    
    public void OnBackClick()
    {
        FindObjectOfType<SplashController>().OnBackFromSelection();
    }

    public void SelectItem(int index)
    {
        m_selectionsButtons.ForEach(x => x.DeselectSprite());
        m_selectionsButtons[index].SelectSprite();
    }

    public void OnPlayClick()
    {
        int selected = m_selectionsButtons.FindIndex(x => x.IsSelected);
        
        switch (selected)
        {
            case -1:
                //Modal must selected one
                break;
            case 0:
                break;
            case 1:
                break;
        }

    }
    
}
