using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public bool inPosition = false;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //inPosition = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //inPosition = false;
    }
    void Update()
    {
        inPosition = check();
    }
    bool check()
    {
        // Get all colliders under the floor1Trigger GameObject
        Collider[] colliders = GetComponentsInChildren<Collider>();

        // Check if any of the player's colliders are overlapping with any of the upper floor colliders
        foreach (Collider collider in colliders)
        {
            if (collider.bounds.Contains(player.transform.position))
                return true;
        }
        return false;
    }
}
