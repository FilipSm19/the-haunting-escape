using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teddyPuzzle : MonoBehaviour
{
    public PuzzleLight light1;
    public PuzzleLight light2;
    public PuzzleLight light3;
    public PuzzleLight light4;
    public PuzzleLight light5;
    public PuzzleLight light6;
    public PuzzleLight light7;
    public GameObject blueLight;
    public GameObject blueLight2;
    public GameObject UItext;
    public GameObject mainCamera;
    public GameObject glass;
    public GameObject teddyBearTrigger;
    private bool puzzleDone = false;


    // Start is called before the first frame update
    void Start()
    {
        teddyBearTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleDone == false)
        {
        }
        if (light1.correctColor && light2.correctColor && light3.correctColor && light4.correctColor && light5.correctColor && light6.correctColor && light7.correctColor && puzzleDone == false)
        {
            puzzleDone = true;
            DisableScript();
            Invoke("DisableLight", 1);
            Invoke("PuzzleComplete", 2);
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
        glass.transform.localPosition = new Vector3(1.16f, -0.11f, 0.47f);
        glass.transform.Rotate(Vector3.right, 75);
        blueLight2.SetActive(true);

    }
}
