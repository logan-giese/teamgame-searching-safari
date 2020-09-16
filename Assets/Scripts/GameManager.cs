using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages static game variables and globally-accessible operations
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    private Light directionalLight;

    /*
        These variables are all the animal prefabs that will be spawned for the levels.
        5 of each animal for the each level. 1 animal every 7 seconds
    */
    public GameObject Elephant;
    public GameObject Giraffe;
    public GameObject Lion;
    public GameObject Rhino;
    public GameObject Frog;
    public GameObject Crocodile;
    private int[] Count = {0,0,0,0,0,0};
    private bool[] InfoFlag = {false,false,false,false,false,false};
    private bool levelIsChanging = false;
    //This variable will be set by the creature if the flags are false 
    private string infoDisplay = "None";
    private float counter = 0f;
    static int level = 1;
    private float timer = 0f;
    //game manager will be told about the flag status by the creatures and be accessed by the character
    //The flag is changed in two locations 1. is in the character animation script for reactions and the other is in the creature script for eating
    //This means that they will change IF AND ONLY IF THE ANIMATOR CONTROLLER IS SET GARRETT YOU BIG IDIOT.
    private int flag = -1;//-1 is no response, 0 is wrong, 1 is correct

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!levelIsChanging && SceneManager.GetActiveScene().name != "MainMenu")
            spawnAnimal();
        checkInfoFlags();
        levelTransition();
        if(flag == 0)
        {
            Debug.Log("Wrong Food");
        }
        else if(flag == 1)
        {
            Debug.Log("Correct Food");
        }
    }
    //This function will run during the update to check if the level has changed
    // to Turn down the directional light to the correct setting
    private void levelTransition()
    {
        if(level == 2 && SceneManager.GetActiveScene().name == "Day_level")
        {
            if(directionalLight.intensity > 10)
            {
                counter += Time.deltaTime;
                //Fade from 1 to 0
                int alpha = (int)Mathf.Lerp(10000, 10, counter / 10);
                directionalLight.intensity = alpha;
            }
            else
            {
                levelIsChanging = false;
                SceneManager.LoadScene("Night_level");
            }
        }
    }
    private void spawnAnimal()
    {
        timer += Time.deltaTime;
        if(timer >= (float)3f)
        {
            int roll = Random.Range(0,4);
            if(level == 2)
            roll = Random.Range(0,6);
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
                //if the level switched from 1 to 2
                if(level == 2)
                {
                    if(roll == 4)
                    {
                        Instantiate(Frog);
                    }
                    else if(roll == 5)
                    {
                        Instantiate(Crocodile);
                    }
                }
                Count[roll]++;
            }

            timer = 0f;
            endCurrentLevel();
        }
    }
    public void endCurrentLevel()
    {
        int count = 0;
        GameObject[] ar = GameObject.FindGameObjectsWithTag("Creature");
        // Debug.Log($"Level {level}");
        foreach(int c in Count)
        {
            count += c;
        }
        if(level == 1)
        {
            //if the array size is 0 and the count is equal to 20 then change
            if(ar.Length == 0 && count == 20)
            {
                // Debug.Log($"Level {level} Finished");
                level = 2;
                levelIsChanging = true;
                //This is getting the directional light for the scene transition
                directionalLight = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
            }
        }
        if(level == 2)
        {
            //if the array size is 0 and the count is equal to 20 then change
            if(ar.Length == 0 && count == 30)
            {
                // Debug.Log($"Level {level} Finished");
                level = 0;
                levelIsChanging = true;
            }
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
    public void setInfoDisplay(string infoDisplay)
    {
        this.infoDisplay = infoDisplay;
    }
    public string getInfoDisplay()
    {
        return infoDisplay;
    }
    public void setInfoFlag(int index, bool infoFlag)
    {
        this.InfoFlag[index] = infoFlag;
    }
    public bool getInfoFlag(int index)
    {
        return InfoFlag[index];
    }
    private void checkInfoFlags()
    {
        //Debug.Log("Checking Flags");
        switch(infoDisplay)
        {
            case "Elephant":
            InfoFlag[0] = true;
            Debug.Log("Hit");
            break;
            case "Lion":
            InfoFlag[1] = true;
            Debug.Log("Hit");
            break;
            case "Giraffe":
            InfoFlag[2] = true;
            Debug.Log("Hit");
            break;
            case "Rhino":
            InfoFlag[3] = true;
            Debug.Log("Hit");
            break;
            case "Frog":
            InfoFlag[4] = true;
            Debug.Log("Hit");
            break;
            case "Crocodile":
            InfoFlag[5] = true;
            Debug.Log("Hit");
            break;
            default:
            break;
        }
    }
}
