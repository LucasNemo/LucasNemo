using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSelectionBehaviour : MonoBehaviour {

    [SerializeField]
    private Sprite m_normalSprite, m_selectedSprite;

    public bool IsSelected { get { return m_selected; } }

    private bool m_selected;

    private Image m_currentImage;
    
    private void Start()
    {
        m_currentImage = GetComponent<Image>();
    }

    public void SelectSprite()
    {
        m_currentImage.sprite = m_selectedSprite;
        m_selected = true;
    }

    public void DeselectSprite()
    {
        m_currentImage.sprite = m_normalSprite;
        m_selected = false;
    }

}
