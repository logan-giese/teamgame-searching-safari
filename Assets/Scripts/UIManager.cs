using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static bool isStartMenu;
    private static float gameVolume;
    public GameObject resetButton;
    public GameObject quitButton;
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        isStartMenu = true;
        resetButton.SetActive(false);
        slider.value = 1;
        gameVolume = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStartMenu)
        {
            resetButton.SetActive(true);
        }
        AudioListener.volume = gameVolume;
    }
    public void playGame()
    {
        if(isStartMenu)
        {
            isStartMenu = false;
            //load first level
        }
        else
        {
            Time.timeScale = 1;
        }
        //make sure to check for value passing between level
    }
    public void changeVolume()
    {
        gameVolume = slider.value;
        //add functionality for the volume level changing
    }
    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //make sure to check for value passing between level
    }
    public void quitGame()
    {
        //simulates the closing of the application
        UnityEditor.EditorApplication.isPlaying = false;
        //will make the application close
        Application.Quit();
    }

    
}
