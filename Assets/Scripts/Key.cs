using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool keyPicked = false;
    public GameObject KeyObj;
    public void Pick()
    {
        keyPicked = true;
        Destroy(KeyObj);

    }
}

