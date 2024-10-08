using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public bool picked = false;
    public GameObject obj;
    public void Pick()
    {
        picked = true;
        obj.SetActive(false);
    }
}
