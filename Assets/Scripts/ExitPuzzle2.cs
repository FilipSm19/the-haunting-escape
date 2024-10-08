using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitPuzzle2 : MonoBehaviour
{//if teddy bear picked, put teddy bear in door hole for teddy bear
 //if key picked use it to open door if teddy bear in hole
    public bool teddyInPlace = false;
    public bool keyInPlace = false;
    public teddyPlace teeddyplace;
    //public Animator animator;
    //public GameObject message = null;

    void Start()
    {
        //if (message) message.SetActive(false);
        //animator.SetBool("isOpen", false);
    }
    void Update()
    {
        if (teeddyplace.teddyInPlace == true && keyInPlace == true)
        {
            StaticData.won = true;
            SceneManager.LoadScene(0);

        }
    }

}
