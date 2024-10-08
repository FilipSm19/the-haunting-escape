using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingRaycast : MonoBehaviour
{
    public int rayLength = 5;
    public LayerMask layerMaskInteract;
    public string excludeLayerName = null;

    private HidingPlace raycastedObj;
    private bool doOnce;
    public string interactTag;

    public GameObject hideText, stopHideText;

    void Start()
    {
        stopHideText.SetActive(false);
        hideText.SetActive(false);
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

                    raycastedObj = hit.collider.gameObject.GetComponent<HidingPlace>();
                }


                if (raycastedObj.hiding == false)
                {
                    hideText.SetActive(true);
                    stopHideText.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        raycastedObj.Hide();
                    }
                }
                else if (raycastedObj.hiding == true)
                {
                    stopHideText.SetActive(true);
                    hideText.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        raycastedObj.Unhide();
                    }
                }
            }


        }

        else
        {
            stopHideText.SetActive(false);
            hideText.SetActive(false);
        }
    }


}