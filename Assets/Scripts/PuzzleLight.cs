using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLight : MonoBehaviour
{
    public bool correctColor = false;
    public string correctColorName = "white";
    public string selectedColor = "white";
    public Light lt;
    public Light floorLt;

    // Start is called before the first frame update
    void Start()
    {
        lt.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedColor == "white") { lt.color = Color.white; floorLt.color = Color.white; }
        else if (selectedColor == "yellow") { lt.color = Color.yellow; floorLt.color = Color.yellow; }
        else if (selectedColor == "green") { lt.color = Color.green; floorLt.color = Color.green; }
        else if (selectedColor == "blue") { lt.color = Color.blue; floorLt.color = Color.blue; }
        else if (selectedColor == "red") { lt.color = Color.red; floorLt.color = Color.red; }

        if (selectedColor == correctColorName) correctColor = true;
        else correctColor = false;
    }
    public void ChangeLight()
    {
        if (selectedColor == "white") selectedColor = "yellow";
        else if (selectedColor == "yellow") selectedColor = "green";
        else if (selectedColor == "green") selectedColor = "blue";
        else if (selectedColor == "blue") selectedColor = "red";
        else selectedColor = "white";
    }
}
