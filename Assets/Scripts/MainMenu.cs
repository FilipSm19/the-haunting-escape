using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    public GameObject controls;
    public float sensitivity;
    public Slider slider;
    public Slider Volslider;
    public GameObject youwon;
    public TextMeshProUGUI youWonText;
    public float volume;
    void Start()
    {
        Cursor.visible = true;
        controls.SetActive(false);
        if (StaticData.sens == -1)
        {
            sensitivity = 150;
        }
        else
        {
            sensitivity = StaticData.sens;
        }

        slider.value = sensitivity / 1000;
        if (StaticData.won == true) youwon.SetActive(true);
        else youwon.SetActive(false);
        if (StaticData.audioVol == -1)
        {
            volume = 0.5f;
            StaticData.audioVol = volume;
        }
        else
        {
            volume = StaticData.audioVol;
        }

        Volslider.value = volume;

        float time = StaticData.elapsedTime;
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        decimal dTime = Convert.ToDecimal(time);
        dTime = dTime - (int)dTime;
        decimal msec = Decimal.Round(dTime, 2, MidpointRounding.AwayFromZero);
        youWonText.text = string.Format("You won! Run time: {0:00} : {1:00} : {2:00}", min, sec, msec);

    }
    void Update()
    {
        if (volume > 1f)
        {
            volume = 1f;
        }
        else if (volume < 0f)
        {
            volume = 0f;
        }
        AudioListener.volume = volume;
    }
    public void changeSens()
    {
        sensitivity = slider.value * 1000;
    }
    public void changeVol()
    {
        volume = Volslider.value;
        StaticData.audioVol = Volslider.value;
    }
    public void PlayGame()
    {
        StaticData.sens = sensitivity;
        StaticData.won = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowControls()
    {
        controls.SetActive(true);
    }
    public void HideControls()
    {
        controls.SetActive(false);
    }
}
