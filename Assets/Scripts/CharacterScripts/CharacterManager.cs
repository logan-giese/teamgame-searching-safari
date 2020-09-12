using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private GameManager gameManager;
    private int isCorrectFood = 0; //-1 is no action, 0 is wrong, 1 is correct
    // Start is called before the first frame update
    void Start()
    {
        //get the game manager to communicate with for feedback to user input
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isCorrectFood = gameManager.getFlag();
    }
    public int feedback()
    {
        return isCorrectFood;
    }
    public void endOfFeedback(int flag)
    {
        gameManager.setFlag(flag);
    }
}
