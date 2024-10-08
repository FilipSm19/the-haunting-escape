using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPuzzle : MonoBehaviour
{//if teddy bear picked, put teddy bear in door hole for teddy bear
 //if key picked use it to open door if teddy bear in hole
    public bool teddyInPlace = false;
    public bool keyInPlace = false;
    public GameObject enemy;
    public Gate gate;
    private bool keyplaced = false;
    public GameObject basementKey;
    public GameObject dialogBox;


    //public Animator animator;
    //public GameObject message = null;

    void Start()
    {
        //if (message) message.SetActive(false);
        //animator.SetBool("isOpen", false);
        basementKey.SetActive(false);
    }
    void Update()
    {
        if (keyInPlace == true && keyplaced == false)
        {

            basementKey.SetActive(true);
            dialogBox.SetActive(false);
            enemy.SetActive(false);
            gate.StartGate();
            gate.GateUse();
            keyplaced = true;

        }
    }

}
