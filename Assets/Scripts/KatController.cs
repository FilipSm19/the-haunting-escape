using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatController : MonoBehaviour
{
public GameObject fireplace;

    public string Tag = "Player";
    public string enemyTag = "Enemy";
    public bool enemyInCollider = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyInCollider = true;
    }
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Tag))
        {
            fireplace.SetActive(true);
        }
        if(other.CompareTag(enemyTag))
        {
             enemyInCollider = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag(Tag))
        {
            fireplace.SetActive(false);

        }
        if(other.CompareTag(enemyTag))
        {
             enemyInCollider = false;
        }
    }
}