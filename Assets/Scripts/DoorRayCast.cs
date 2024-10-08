using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRayCast : MonoBehaviour
{
    public int rayLength = 5;
    public LayerMask layerMaskInteract;
    public string excludeLayerName = null;

    private Door raycastedObj;
    private KeyCode openDoorKey = KeyCode.E;
    private bool doOnce;
    public string interactTag;
    public GameObject message;
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

                    raycastedObj = hit.collider.gameObject.GetComponent<Door>();
                }
                if (raycastedObj.doorOpen == false)
                    message.SetActive(true);
                if (Input.GetKeyDown(openDoorKey))
                {
                    raycastedObj.PlayAnimation();
                }
            }

        }
        else message.SetActive(false);
    }

}
