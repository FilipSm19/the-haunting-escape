using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRaycast : MonoBehaviour
{
    public int rayLength = 5;
    public LayerMask layerMaskInteract;
    public string excludeLayerName = null;

    private keyLock keyObj;
    private teddyPlace teddyObj;
    private KeyCode interact = KeyCode.E;
    private bool doOnce;
    public string teddyPlace;
    public string keyPlace;
    public GameObject messageKey;
    public GameObject messageTeddy;
    private bool hideText;

    void Start()
    {
        messageKey.SetActive(false);
        messageTeddy.SetActive(false);
    }

    void Update()
    {

        Vector3 mousePostion = Input.mousePosition;
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(keyPlace))
            {

                if (!doOnce)
                {

                    keyObj = hit.collider.gameObject.GetComponent<keyLock>();
                }


                if (hideText == false && keyObj.keyInPlace == false)
                    messageKey.SetActive(true);
                else
                    messageKey.SetActive(false);

                if (Input.GetKeyDown(interact))
                {
                    keyObj.Interact();
                    if (keyObj.keyInPlace == true) messageKey.SetActive(false);
                    StartCoroutine(HideText());
                }
            }
            else if (hit.collider.CompareTag(teddyPlace))
            {


                if (!doOnce)
                {

                    teddyObj = hit.collider.gameObject.GetComponent<teddyPlace>();
                }
                if (hideText == false && teddyObj.teddyInPlace == false)
                    messageTeddy.SetActive(true);
                else
                    messageTeddy.SetActive(false);

                if (Input.GetKeyDown(interact))
                {
                    teddyObj.Interact();
                    StartCoroutine(HideText());
                }
            }





        }
        else
        {
            messageTeddy.SetActive(false);
            messageKey.SetActive(false);
        }

    }
    IEnumerator HideText()
    {

        hideText = true;

        yield return new WaitForSeconds(3);
        hideText = false;

    }

}
