using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizemljeController : MonoBehaviour
{
    public GameObject fireplace;
    public bool enemyInCollider = true;
    public string Tag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Tag))
        {
            fireplace.SetActive(true);

        }
    }
    void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag(Tag))
        {
            fireplace.SetActive(false);

        }
    }
    }
