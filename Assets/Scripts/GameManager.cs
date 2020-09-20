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
    //Variable for all animals that are correctly fed
    private int score = 0;
    //The Variable that determines if the level is changed 
    private int ConditionalPoints = 0;
    private bool levelIsChanging = false;
    //This variable will be set by the creature if the flags are false 
    private string infoDisplay = "None";
    private float counter = 0f;
    static int level = 1;
    private float timer = 0f;
    //game manager will be told about the flag status by the creatures and be accessed by the character
    //The flag is changed in one location that is not the game manager 1. is in the character animation script for reactions
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
            {
                spawnAnimal();
                checkInfoFlags();
            }
        if(levelIsChanging)
            levelTransition();
    }
    //This function will run during the update to check if the level has changed
    // to Turn down the directional light to the correct setting
    private void levelTransition()
    {
        directionalLight = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
        if(level == 2 && levelIsChanging)
        {
            if(directionalLight.intensity > 10)
            {
                counter += Time.deltaTime;
                //Fade from 1 to 0
                int alpha = (int)Mathf.Lerp(10000, 10, counter / 4.5f);
                directionalLight.intensity = alpha;
            }
            else
            {
                levelIsChanging = false;
                SceneManager.LoadScene("Night_level");
            }
        }
        else if(level == 0 && levelIsChanging)
        {
            //reset everything in case they want to play again
            level = 1;
            levelIsChanging = false;
            SceneManager.LoadScene("MainMenu");
        }
    }
    /*
    This function will constantly spawn animals. It will only spawn one kind, but if they are despawned they will respawn the animal.
    */
    private void spawnAnimal()
    {
        timer += Time.deltaTime;
        if(timer >= (float)5f)
        {
            if(Count[0] < 1)
            {
                //spawn Elephant
                Instantiate(Elephant);
                Count[0]++;
            }
            else if(Count[1] < 1)
            {
                //spawn Giraffe
                Instantiate(Giraffe);
                Count[1]++;
            }
            else if(Count[2] < 1)
            {
                //spawn Lion
                Instantiate(Lion);
                Count[2]++;
            }
            else if(Count[3] < 1)
            {
                //spawn Rhino
                Instantiate(Rhino);
                Count[3]++;
            }
            //if the level switched from 1 to 2
            if(level == 2)
            {
                if(Count[4] < 1)
                {
                    Instantiate(Frog);
                    Count[4]++;
                }
                else if(Count[5] < 1)
                {
                    Instantiate(Crocodile);
                    Count[5]++;
                }
            }

            timer = 0f;
            endCurrentLevel();
        }
    }
    /*
    The endCurrentLevel function will check to see if the about of required animals was his by the player.
    if the level is one then we should be setting level to 2, raising the levelIsChanging flag, reseting the ConditionalPoints variable, and reseting the Count array
    if the level is two then we set level to 0, raise the levelIsChanging flag, reset the conditionalAnimal, and rest the InfoFlag and Count arrays
    */
    public void endCurrentLevel()
    {
        //Level condition check
        if(ConditionalPoints >= 1)
        {
            //Level One
            if(level == 1)
            {
                level = 2;
                levelIsChanging = true;
                ConditionalPoints = 0;
                Count = new int[] {0,0,0,0,0,0};
            }
            //Level Two
            else
            {
                level = 0;
                levelIsChanging = true;
                ConditionalPoints = 0;
                score = 0;
                Count = new int[] {0,0,0,0,0,0};
                InfoFlag = new bool[] { false, false, false, false, false, false };
            }
        }
    }
    /*
    The animalHit function will take an index and a flag to decrease the count for the animal index and determine if the food was correct
    */
    public void animalHit(int index, bool flag, string creature, string food)
    {
        if(flag)
        {
            //add to the ConditionalPoints if flag is true and set the flag for the infoFlags
            if(level == 1 && food == "meat")
                ConditionalPoints++;
            else if(level == 2 && food == "broccoli")
                ConditionalPoints++;
            if(!InfoFlag[index])
                {
                    // InfoFlag[index] = true;
                    infoDisplay = creature;
                }
            //set this.flag = true
            this.flag = 1;
            //increase the score count
            score++;
        }
        else
        {
            //set this.flag = false
            this.flag = 0;
        }
    }
    public int getConditionalPoints()
    {
        return ConditionalPoints;
    }
    public int getScore()
    {
        return score;
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
    public void decreaseCount(int index)
    {
        Count[index]--;
    }
    /*
    The checkInfoFlags function will check to see which animal was hit and sets the infoDisplay to that animal.
    */
    private void checkInfoFlags()
    {
        switch(infoDisplay)
        {
            case "Elephant":
            InfoFlag[0] = true;
            break;
            case "Lion":
            InfoFlag[1] = true;
            break;
            case "Giraffe":
            InfoFlag[2] = true;
            break;
            case "Rhino":
            InfoFlag[3] = true;
            break;
            case "Frog":
            InfoFlag[4] = true;
            break;
            case "Crocodile":
            InfoFlag[5] = true;
            break;
            default:
            break;
        }
    }
}
