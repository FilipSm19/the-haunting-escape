using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRaycast : MonoBehaviour
{
    public int rayLength = 10;
    public LayerMask layerMaskInteract;
    public string excludeLayerName = null;

    private Key raycastedObj;
    private KeyCode pickKey = KeyCode.E;
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

                    raycastedObj = hit.collider.gameObject.GetComponent<Key>();
                }
                if (raycastedObj.keyPicked == false) message.SetActive(true);
                if (Input.GetKeyDown(pickKey))
                {
                    raycastedObj.Pick();
                }
            }
        }

        else
        {
            message.SetActive(false);
        }

    }
}
