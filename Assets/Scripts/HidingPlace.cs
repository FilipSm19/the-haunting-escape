using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    public GameObject player, hidingPlayer;
    public EnemyController monsterScript;
    public Transform monsterTransform;
    public float loseDistance;
    public bool hiding;

    void Start()
    {
        hiding = false;
    }
    /* void OnTriggerStay(Collider other)
     {
         if (other.CompareTag("MainCamera"))
         {
             hideText.SetActive(true);
             interactable = true;
         }
     }
     void OnTriggerExit(Collider other)
     {
         if (other.CompareTag("MainCamera"))
         {
             hideText.SetActive(false);
             interactable = false;
         }
     }*/

    // Update is called once per frame
    void Update()
    {
        monsterScript.hiding = hiding;
    }
    public void Hide()
    {
        hidingPlayer.SetActive(true);
        float distance = Vector3.Distance(monsterTransform.position, player.transform.position);
        if (distance > loseDistance)
        {
            if (monsterScript.enemyChase == true)
            {
                
                monsterScript.stopChase();
                
            }
        }
        hiding = true;
        player.SetActive(false);
    }
    public void Unhide()
    {
        player.SetActive(true);
        hidingPlayer.SetActive(false);
        hiding = false;
    }
}
