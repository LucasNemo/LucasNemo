using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image m_image;
    [SerializeField]
    private Text m_text;
    [SerializeField]
    private Material m_grayMaterial;

    private string m_strokeString;
    private string m_normalString;

    private void Start()
    {
        m_normalString = m_text.text;
        m_strokeString = StrikeThrough(m_text.text);
    }

    public void OnToggle(bool toggle)
    {
        if (toggle)
        {
            m_image.material = m_grayMaterial;
            m_text.text = m_strokeString;
        }
        else
        {
            m_image.material = null;
            m_text.text = m_normalString;
        }
    }

    private string StrikeThrough(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }

}
