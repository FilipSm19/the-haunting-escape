using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public string Tag = "Player";
    public GameObject dialog;
    public DialogBox previousDialogTrigger = null;
    private bool triggered;
    public bool repeat = false;
    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
        triggered = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Tag) && triggered == false)
        {
            if(repeat == false) triggered = true;
            previousDialogTrigger.StopCoroutine(showDialog());
            previousDialogTrigger.dialog.SetActive(false);
            StartCoroutine(showDialog());
        }
    }
    IEnumerator showDialog()
    {
        dialog.SetActive(true);
        yield return new WaitForSeconds(4);
        dialog.SetActive(false);
    }
}
