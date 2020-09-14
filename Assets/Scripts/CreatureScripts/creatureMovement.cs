using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureMovement : MonoBehaviour
{
    private Vector3 spawnLocation;
    private Vector3 currentLocation;
    private Vector3 endLocations;
    private Vector3 scale;
    private GameManager gameManager;
    // private int flag = -1; //-1 is no action, 0 is wrong, 1 is correct
    private double speed = 1.75;
    public string food;
    public string Creature;
    public int InfoIndex;
    private bool isInArea = false;
    private bool isCorrectFood = false;
    // Start is called before the first frame update
    void Start()
    {
        // //roll for which side to spawn on
        spawn();
        // //get position
        spawnLocation = transform.position;
        currentLocation = spawnLocation;

        //get the game manager to communicate with for feedback to user input
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        //if the object is out of bounds
        if (spawnLocation.x == -40f && currentLocation.x > 40f)
            Destroy(this.gameObject);
        //else if spawned on right side
        else if (spawnLocation.x == 40f && currentLocation.x < -40f)
            Destroy(this.gameObject);
    }
    public void spawn()
    {
        int roll = Random.Range(1, 100);
        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
        //if even left
        if (roll % 2 == 0)
        {
            // Debug.Log("Left Spawn");
            transform.position = new Vector3(-40f, 0f, Random.Range(40f, 60f));
        }
        //else right
        else
        {
            // Debug.Log("Right Spawn");
            transform.position = new Vector3(40f, 0f, Random.Range(30f, 20f));
            //rotate 180 degrees for the right side
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
    }
    public void move()
    {
        //get current location for the update
        currentLocation = transform.position;
        //if spawned on left side
        if (spawnLocation.x == -40f)
            currentLocation.x += (float)speed * Time.deltaTime;
        //else if spawned on right side
        else if (spawnLocation.x == 40f)
            currentLocation.x -= (float)speed * Time.deltaTime;
        //else the tutorial for the player 

        //set the new position
        transform.position = currentLocation;
    }
    //scales for the different animals
    //elephant: 3.6,1.9,1.5
    public void setIsInArea(bool flag)
    {
        isInArea = flag;
    }
    public bool getIsInArea()
    {
        return isInArea;
    }
    public void setIsCorrectFood(bool flag)
    {
        isCorrectFood = flag;
    }
    public bool getIsCorrectFood()
    {
        return isCorrectFood;
    }
    public void setSpeed(double speed)
    {
        this.speed = speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "broccoli" || other.tag == "meat")
        {
            isInArea = true;
            if (other.tag == food)
            {
                isCorrectFood = true;
            }
            else
            {
                isCorrectFood = false;
            }
            Destroy(other.gameObject);
            setInfoDisplay();
        }
    }
    //This function will see if the flag for the the creature has already been set and then sets the infoDisplay to that creature
    public void setInfoDisplay()
    {
        if(!gameManager.getInfoFlag(InfoIndex))
        {
            gameManager.setInfoDisplay(Creature);
        }
    }
}
