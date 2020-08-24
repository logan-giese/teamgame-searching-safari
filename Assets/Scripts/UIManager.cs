using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStartMenu)
        {
            resetButton.SetActive(true);
        }
    }
    public void playGame()
    {
        if(isStartMenu)
        {
            // Debug.Log("Here");
            isStartMenu = false;
        }
        //Continue to current or first level
        //make sure to check for value passing between level
    }
    public void changeVolume()
    {
        gameVolume = slider.value;
        //add functionality for the volume level changing
    }
    public void resetLevel()
    {
        Debug.Log("Reset");
        //functionality for the level changing and the 
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
