using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teddyPuzzleEasy : MonoBehaviour
{
    public PuzzleLight light2;
    public PuzzleLight light4;
    public PuzzleLight light5;
    public PuzzleLight light7;
    public GameObject blueLight;
    public GameObject blueLight2;
    public GameObject UItext;
    public GameObject mainCamera;
    public GameObject glass;
    public GameObject teddyBearTrigger;
    private bool puzzleDone = false;
    private bool invoked = false;
    public AudioSource musicEffect;


    // Start is called before the first frame update
    void Start()
    {
        teddyBearTrigger.SetActive(false);
        invoked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleDone == false)
        {
        }
        if (light2.correctColor && light4.correctColor && light5.correctColor && light7.correctColor && puzzleDone == false)
        {
            puzzleDone = true;
            DisableScript();

            musicEffect.Play();
            Invoke("DisableLight", 1);
            Invoke("PuzzleComplete", 2);

        }
        if (invoked == true)
        {
            glass.transform.localPosition = Vector3.Lerp(glass.transform.localPosition, new Vector3(1.16f, -0.11f, 0.47f), Time.deltaTime);
            glass.transform.rotation = Quaternion.Lerp(glass.transform.rotation, Quaternion.Euler(75, 180, 0), Time.deltaTime);
        }
    }
    public void DisableLight()
    {
        blueLight.SetActive(false);
    }
    public void DisableScript()
    {
        mainCamera.GetComponent<LightsPuzzleRaycast>().enabled = false;
        UItext.SetActive(false);

    }
    public void PuzzleComplete()
    {
        teddyBearTrigger.SetActive(true);
        blueLight2.SetActive(true);
        invoked = true;

    }
}
