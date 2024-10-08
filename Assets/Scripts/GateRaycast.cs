using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateRaycast : MonoBehaviour
{
    public int rayLength = 5;
    public LayerMask layerMaskInteract;
    public string excludeLayerName = null;

    private Gate raycastedObj;
    private KeyCode openGateKey = KeyCode.E;
    private bool doOnce;
    public string interactTag;
    public GameObject message_examine;
    public GameObject message_putLever;
    public GameObject message_use;
    public PickableObject lever;

    public GameObject pickableLever;
    public GameObject leverLight;

    private bool keyPressed;

    private void Start()
    {
        pickableLever.GetComponent<CapsuleCollider>().enabled = false;
        leverLight.SetActive(false);
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

                    raycastedObj = hit.collider.gameObject.GetComponent<Gate>();
                }
                if (raycastedObj.leverPlaced == false && lever.picked == false && keyPressed == false)
                {
                    message_examine.SetActive(true);
                }
                else if (raycastedObj.leverPlaced == false && lever.picked && keyPressed == false)
                {
                    message_putLever.SetActive(true);
                }
                else if (keyPressed == false) message_use.SetActive(true);


                if (Input.GetKeyDown(openGateKey) && keyPressed == false)
                {
                    pickableLever.GetComponent<CapsuleCollider>().enabled = true;
                    leverLight.SetActive(true);
                    hideMessages();
                    raycastedObj.GateUse();
                    if (raycastedObj.leverPlaced == false)
                    {
                        StartCoroutine(showMessages());
                        keyPressed = true;
                    }

                }
            }


        }
        else hideMessages();
    }
    IEnumerator showMessages()
    {
        yield return new WaitForSeconds(3);
        keyPressed = false;
    }
    void hideMessages()
    {
        message_use.SetActive(false);
        message_examine.SetActive(false);
        message_putLever.SetActive(false);
    }

}
