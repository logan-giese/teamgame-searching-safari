using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static bool isStartMenu = true;
    private static float gameVolume = 1;
    public GameObject resetButton;
    public GameObject quitButton;
    public Slider slider;

    public GameObject canvas;
    private bool menuVisible = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isStartMenu)
        {
            resetButton.SetActive(false);
        }
        else
        {
            resetButton.SetActive(true);
            isStartMenu = false;
            toggleMenu();
        }
        slider.value = gameVolume;
        AudioListener.volume = gameVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStartMenu)
        {
            // Show or hide the pause menu in-game
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                toggleMenu();
            }
        }
    }
    public void playGame()
    {
        if (isStartMenu)
        {
            isStartMenu = false;
            SceneManager.LoadScene(1);
            //load first level
        }
        else
        {
            toggleMenu();
        }
        //make sure to check for value passing between level
    }
    public void changeVolume()
    {
        gameVolume = slider.value;
        AudioListener.volume = gameVolume;
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
    public void toggleMenu()
    {
        // Show or hide the menu based on its current state
        if (menuVisible)
        {
            canvas.SetActive(false);
            menuVisible = false;
            Time.timeScale = 1;
        }
        else
        {
            canvas.SetActive(true);
            menuVisible = true;
            Time.timeScale = 0;
        }
    }

    
}
