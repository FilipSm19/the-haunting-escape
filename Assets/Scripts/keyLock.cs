using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyLock : MonoBehaviour
{
    public bool keyInPlace = false;
    public GameObject message = null;
    public GameObject KeyObject;
    public ExitPuzzle puzzleScript;
    public Key key = null;

    void Start()
    {
        KeyObject.SetActive(false);
        if (message) message.SetActive(false);
    }

    public void Interact()
    {

        if (!keyInPlace && key.keyPicked)
        {

            keyInPlace = true;
            puzzleScript.keyInPlace = true;
            KeyObject.SetActive(true);
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
