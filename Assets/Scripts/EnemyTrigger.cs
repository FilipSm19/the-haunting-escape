using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public Key key = null;
    public bool triggered = false;
    public string Tag = "Player";
    public PlayerMovement player;
    public GameObject cameraPos;
    public PlayerCamera cameraScript;
    public Transform enemy;
    public GameObject message;
    private float i = 0;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }
    void Update()
    {

        if (triggered == true && i < 5)
        {

            i += Time.deltaTime;
            if (i > 2 && i < 3)
                cameraPos.transform.LookAt(enemy);
        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Tag))
        {

            if (key.keyPicked && triggered == false)
            {
                triggered = true;

                StartCoroutine(player.Quiet(player.footsteps, player.running));
                player.allowMovement = false;
                cameraScript.enabled = false;

                Invoke("StartMoving", 5);


            }
        }
    }
    private void StartMoving()
    {
        player.allowMovement = true;
        cameraScript.enabled = true;
        message.SetActive(true);
        Invoke("HideText", 3);
    }
    private void HideText()
    {
        message.SetActive(false);
    }
}
