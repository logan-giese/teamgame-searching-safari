using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureMovement : MonoBehaviour
{
    private Vector3 spawnLocation;
    private Vector3 currentLocation;
    private Vector3 endLocations;
    private Vector3 scale;
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
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    public void spawn()
    {
        int roll = Random.Range(1, 100);
        //if even left
        if (roll % 2 == 0)
        {
            // Debug.Log("Left Spawn");
            transform.position = new Vector3(-40f, 1.2f, Random.Range(40f, 60f));
        }
        //else right
        else
        {
            // Debug.Log("Right Spawn");
            transform.position = new Vector3(40f, 1.2f, Random.Range(30f, 20f));
        }
    }
    public void move()
    {
        //get current location for the update
        currentLocation = transform.position;
        //if spawned on left side
        if (spawnLocation.x == -40f)
            currentLocation.x += 5f * Time.deltaTime;
        //else if spawned on right side
        else if (spawnLocation.x == 40f)
            currentLocation.x -= 5f * Time.deltaTime;
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.tag == "broccoli" || other.tag == "meat")
        {
            isInArea = true;
            if (other.tag == "broccoli")
            {
                isCorrectFood = true;
            }
            else
            {
                isCorrectFood = false;
            }
        }
    }
}
