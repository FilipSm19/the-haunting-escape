using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsPuzzleRaycast : MonoBehaviour
{
    public int rayLength = 10;
    public LayerMask layerMaskInteract;
    public LayerMask layerMaskRead;
    public string excludeLayerName = null;

    private PuzzleLight raycastedObj;
    private KeyCode pressE = KeyCode.E;
    private bool doOnce;
    public GameObject message;
    public GameObject clueMessage;
    public GameObject readMessage;
    private bool messageRead;

    public playerTrigger playerTrig;

    void Start()
    {
        message.SetActive(false);
        clueMessage.SetActive(false);
        readMessage.SetActive(false);
        messageRead = false;
    }

    void Update()
    {
        Vector3 mousePostion = Input.mousePosition;
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
        int readmask = layerMaskRead.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask) && playerTrig.inPosition == true)
        {

            if (!doOnce)
            {

                raycastedObj = hit.collider.gameObject.GetComponent<PuzzleLight>();
            }
            message.SetActive(true);
            if (Input.GetKeyDown(pressE))
            {
                raycastedObj.ChangeLight();
            }


        }
        else if (Physics.Raycast(transform.position, fwd, out hit, rayLength, readmask))
        {
            if (Input.GetKeyDown(pressE))
            {
                messageRead = true;
                readMessage.SetActive(false);
            }
            if (messageRead == true)
            {
                clueMessage.SetActive(true);
            }
            else
            {
                readMessage.SetActive(true);
            }
        }

        else
        {
            message.SetActive(false);
            clueMessage.SetActive(false);
            readMessage.SetActive(false);
        }

    }
}
