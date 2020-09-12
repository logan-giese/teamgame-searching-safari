using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages static game variables and globally-accessible operations
/// </summary>
public class GameManager : MonoBehaviour
{
    /*
        These variables are all the animal prefabs that will be spawned for the levels.
        5 of each animal for the each level. 1 animal every 7 seconds
    */
    public GameObject Elephant;
    public GameObject Giraffe;
    public GameObject Lion;
    public GameObject Rhino;
    private int[] Count = {0,0,0,0};
    private bool allDestroyed = false;
    private float timer = 0f;
    //game manager will be told about the flag status by the creatures and be accessed by the character
    private int flag = -1;//-1 is no response,0 is wrong, 1 is correct
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= (float)7f)
        {
            int roll = Random.Range(0,4);
            if(Count[roll] < 5)
            {
                if(roll == 0)
                {
                    //spawn Elephant
                    Instantiate(Elephant);
                }
                else if(roll == 1)
                {
                    //spawn Giraffe
                    Instantiate(Giraffe);
                }
                else if(roll == 2)
                {
                    //spawn Lion
                    Instantiate(Lion);
                }
                else if(roll == 3)
                {
                    //spawn Rhino
                    Instantiate(Rhino);
                }
                Count[roll]++;
            }

            timer = 0f;
        }
    }
    public int getFlag()
    {
        return flag;
    }
    public void setFlag(int flag)
    {
        this.flag = flag;
    }
}
