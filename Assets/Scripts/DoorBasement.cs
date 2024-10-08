using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBasement : MonoBehaviour
{
    public bool doorOpen = false;
    public GameObject message = null;
    public PickableObject key = null;
    public AudioSource openAudio;
    public AudioSource lockedAudio;
    private float i = 0;

    void Start()
    {
        if (message) message.SetActive(false);

    }
    void Update()
    {
        bool loaded = false;
        if (doorOpen)
        {

            i += Time.deltaTime;
            if (i > 1.5f && loaded == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                loaded = true;
            }

        }
    }

    public void PlayAnimation()
    {


        if (!doorOpen && key.picked)
        {

            doorOpen = true;
            openAudio.Play();


        }
        else
        {
            lockedAudio.Play();
            message.SetActive(true);
            Invoke("HideText", 3);
        }
    }

    private void HideText()
    {
        message.SetActive(false);
    }
}