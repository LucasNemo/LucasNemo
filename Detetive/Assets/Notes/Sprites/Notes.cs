using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour {

    public static Notes instance; 

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public Animator animator; 

    public void NotesClicked()
    {
        animator.SetTrigger("ChangeState");
    }

    public void Remove()
    {
        instance = null;
        Destroy(gameObject);   
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
