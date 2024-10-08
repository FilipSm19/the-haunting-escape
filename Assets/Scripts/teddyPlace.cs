using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teddyPlace : MonoBehaviour
{
    public bool teddyInPlace = false;
    public GameObject message = null;
    public GameObject TeddyObject;
    public Key key = null;
    public ExitPuzzle puzzleScript;

    void Start()
    {
        TeddyObject.SetActive(false);
        if (message) message.SetActive(false);
    }

    public void Interact()
    {

        if (!teddyInPlace && key.keyPicked)
        {

            teddyInPlace = true;
            puzzleScript.teddyInPlace = true;
            TeddyObject.SetActive(true);
        }
        else if (!key.keyPicked && message)
        {
            StartCoroutine(ShowText());
        }

    }
    IEnumerator ShowText()
    {
        message.SetActive(true);
        yield return new WaitForSeconds(3);
        message.SetActive(false);

    }
}
