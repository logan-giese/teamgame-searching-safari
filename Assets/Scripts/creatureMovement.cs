using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureMovement : MonoBehaviour
{
    private Vector3 spawnLocation;
    private Vector3 currentLocation;
    private Vector3 endLocation = new Vector3(-3f,0f,22f);
    // Start is called before the first frame update
    void Start()
    {
        //roll for which side to spawn on
        randomSpawn();
        //get position
        spawnLocation = transform.position;
        currentLocation = spawnLocation;
    }

    // Update is called once per frame
    void Update()
    {
        //get current location for the update
        currentLocation = transform.position;
       //if spawned on left side
       if(spawnLocation.x == -40f)
        currentLocation.x += 5f * Time.deltaTime;
       //else if spawned on right side
       else if(spawnLocation.x == 40f)
        currentLocation.x -= 5f * Time.deltaTime;
       //else the tutorial for the player 

       //set the new position
        transform.position = currentLocation;
    }
    private void randomSpawn()
    {
        int roll = Random.Range(1,100);
        //if even left
        if(roll % 2 == 0)
        {
            Debug.Log("Left");
            transform.position = new Vector3(-40f, 0.5f,Random.Range(40f,60f));
        }
        //else right
        else
        {
            Debug.Log("Right");
            transform.position = new Vector3(40f, 0.5f,Random.Range(30f,20f));
        }
    }
}
