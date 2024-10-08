using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool doorOpen = false;
    public Animator animator;
    public Animator animatorLever = null;
    public GameObject message = null;
    public GameObject message2 = null;

    public bool leverPlaced = false;
    public PickableObject leverPick;
    public GameObject lever;
    public GameObject player;
    public GameObject gateCamera;

    public AudioSource gateOpen;
    public AudioSource gateClose;
    public bool gateWorks = true;
    public bool showLever = false;


    void Start()
    {
        if (message) message.SetActive(false);
        animator.SetBool("isOpen", false);
        if (animatorLever) animatorLever.SetBool("LeverUp", false);
        lever.GetComponent<MeshRenderer>().enabled = false;
        gateCamera.SetActive(false);
        gateWorks = true;
        if (showLever) lever.GetComponent<MeshRenderer>().enabled = true;
    }
    public void CameraChange()
    {
        player.SetActive(false);
        gateCamera.SetActive(true);
        StartCoroutine(returnPlayer());
    }
    IEnumerator returnPlayer()
    {
        yield return new WaitForSeconds(3);
        gateCamera.SetActive(false);
        player.SetActive(true);
    }
    public void GateUse()
    {
        if (leverPlaced)
        {
            if (!doorOpen && gateWorks)
            {
                gateOpen.Play();
                animator.SetBool("isOpen", true);
                animatorLever.SetBool("LeverUp", true);
                doorOpen = true;
                CameraChange();


            }
            else if (!doorOpen && !gateWorks)
            {
                message2.SetActive(true);
                Invoke("HideText", 3);
            }
            else
            {
                gateClose.Play();
                animator.SetBool("isOpen", false);
                animatorLever.SetBool("LeverUp", false);
                doorOpen = false;
                CameraChange();

            }
        }
        else if (leverPick.picked)
        {
            lever.GetComponent<MeshRenderer>().enabled = true;
            leverPlaced = true;
        }
        else
        {
            message.SetActive(true);
            Invoke("HideText", 3);
        }
    }

    private void HideText()
    {
        message.SetActive(false);
        message2.SetActive(false);
    }
    public void StopGate()
    {
        gateWorks = false;
    }
    public void StartGate()
    {
        gateWorks = true;
    }
}
