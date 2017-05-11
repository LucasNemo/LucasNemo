using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour {


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public Animator animator; 

    public void NotesClicked()
    {
        animator.SetTrigger("ChangeState");
    }
}
