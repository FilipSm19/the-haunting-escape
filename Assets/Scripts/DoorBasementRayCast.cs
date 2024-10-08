using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBasementRayCast : MonoBehaviour
{
    public int rayLength = 5;
    public LayerMask layerMaskInteract;
    public string excludeLayerName = null;

    private DoorBasement raycastedObj;
    private KeyCode openDoorKey = KeyCode.E;
    private bool doOnce;
    public string interactTag;
    public GameObject message;
    public bool tryOpen;
    void Start()
    {
        message.SetActive(false);
    }

    void Update()
    {
        Vector3 mousePostion = Input.mousePosition;
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactTag))
            {

                if (!doOnce)
                {

                    raycastedObj = hit.collider.gameObject.GetComponent<DoorBasement>();

                }
                if (raycastedObj.doorOpen == false && tryOpen == false)
                {
                    message.SetActive(true);

                }
                if (Input.GetKeyDown(openDoorKey))
                {
                    tryOpen = true;
                    message.SetActive(false);
                    Invoke("resetOpen", 3);
                    raycastedObj.PlayAnimation();
                }
            }

        }
        else message.SetActive(false);
    }
    public void resetOpen()
    {
        tryOpen = false;
    }

}
