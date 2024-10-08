using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    private bool doorOpen = false;
    public GameObject message = null;
    public Animator animator;
    public Key key;
    void Start()
    {   if(message) message.SetActive(false);
        animator.SetBool("isOpen", false);
    }
    
    public void PlayAnimation()
    {
        if(!doorOpen && key.keyPicked)
        {
            animator.SetBool("isOpen", true);
            doorOpen = true;
        }
        else if (key.keyPicked)
        {
            animator.SetBool("isOpen", false);
            doorOpen = false;
        }
        else
        {   message.SetActive(true);
            Invoke("message.SetActive(false)", 3);
            print("No key");
        }

    }
}