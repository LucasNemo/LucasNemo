using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Notes : MonoBehaviour {

    [SerializeField]
    private List<GameObject> m_pages;
    [SerializeField]
    private GameObject m_notes;
    public static Notes instance; 
    
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public Animator animator; 

    public void NotesClicked()
    {
        m_notes.SetActive(!m_notes.activeSelf);
        //animator.SetTrigger("ChangeState");
    }

    public void Remove()
    {
        instance = null;
        Destroy(gameObject);   
    }
    
    public void Show()
    {
        m_notes.SetActive(true);
    }

    public void Hide()
    {
        m_notes.SetActive(false);
    }

    public void ChangePage(int page)
    {
        m_pages.ForEach(x => x.SetActive(false));
        m_pages[page].SetActive(true);
    }
}
