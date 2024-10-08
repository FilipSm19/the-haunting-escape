using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool doorOpen = false;
    public Animator animator;
    public Animator animatorLever = null;
    public GameObject message = null;
    public Key key = null;
    public AudioSource openAudio;

    void Start()
    {
        if (message) message.SetActive(false);
        animator.SetBool("isOpen", false);
        if (animatorLever) animatorLever.SetBool("LeverUp", false);
    }

    public void PlayAnimation()
    {
        if (!key)
        {
            if (!doorOpen)
            {
                openAudio.Play();
                animator.SetBool("isOpen", true);
                //animatorLever.SetBool("LeverUp", true);
                doorOpen = true;

            }
            /*else
            {
                animator.SetBool("isOpen", false);
                //animatorLever.SetBool("LeverUp", false);
                doorOpen = false;
            }*/
        }
        else
        {
            if (!doorOpen && key.keyPicked)
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
            {
                message.SetActive(true);
                Invoke("HideText", 3);
            }
        }
    }
    private void HideText()
    {
        message.SetActive(false);
    }
}