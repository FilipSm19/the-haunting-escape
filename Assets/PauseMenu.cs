using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject controls;
    public GameObject pauseMenu;
    public float sensitivity;
    public float volume;
    public Slider slider;
    public Slider Volslider;
    bool menuActive = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        controls.SetActive(false);
        pauseMenu.SetActive(false);
        if (StaticData.sens == -1)
        {
            sensitivity = 150;
        }
        else
        {
            sensitivity = StaticData.sens;
        }
        slider.value = sensitivity / 1000;
        Time.timeScale = 1;
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
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuActive == false) showMenu();
            else hideMenu();
        }

        if (menuActive == false)
        {
            StaticData.elapsedTime += Time.deltaTime;
        }

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
    public void showMenu()
    {
        menuActive = true;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void hideMenu()
    {
        menuActive = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        HideControls();
    }
    public void changeSens()
    {
        sensitivity = slider.value * 1000;
        StaticData.sens = sensitivity;
    }
    public void changeVol()
    {
        volume = Volslider.value;
        StaticData.audioVol = Volslider.value;
    }
    public void ExitGame()
    {
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
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

    // Update is called once per frame

}
