using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the in-game UI components and relevant variables
/// </summary>
public class GameplayUIManager : MonoBehaviour
{
    // The GameManager used for global communication
    private GameManager gm;

    // The throw script to send throw type selections to
    public ThrowScript throwScript;

    // The images to use for in-game crosshairs
    public Texture2D defaultCrosshairImage;
    public Texture2D meatCrosshairImage;
    public Texture2D broccoliCrosshairImage;

    // Throw type buttons (already placed in the UI canvas) to use for selecting and indicating throw type
    public GameObject meatButton;
    public GameObject broccoliButton;

    // The text object for displaying helpful information
    public Text assistantText;

    // The popup box object for displaying animal information/facts
    public GameObject popupBox;

    /// <summary>
    /// Available types of object to throw
    /// </summary>
    public enum ThrowType { NONE, MEAT, BROCCOLI };

    /// <summary>
    /// Currently selected type of object to throw
    /// </summary>
    ThrowType throwType = ThrowType.NONE;

    // Start is called before the first frame update
    void Start()
    {
        // Get the game manager object from the scene
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Set the in-game cursor to a crosshair sprite based on the current throw type (and set indicators to disabled)
        SetThrowIndicators();

        // Hide the popup box to start with
        popupBox.SetActive(false);

        // Play the intro clip when starting level 1
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            DialogueScript.PlayClip(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if an animal has been selected (correct food given)
        string selectedAnimal = gm.getInfoDisplay();
        int correctFlag = gm.getFlag();

        if (selectedAnimal != "None")
        {
            // Check if a wrong animal was selected
            if (correctFlag == 0)
            {
                SetAssistantText("WRONGAMUNDO! Try again!");
                DialogueScript.PlayClip(6);
            }
            else
            {
                Text infoTitle = popupBox.transform.GetChild(0).gameObject.GetComponent<Text>(); // Get info title (1st child)
                Text infoDescription = popupBox.transform.GetChild(1).gameObject.GetComponent<Text>(); // Get info description (2nd child)
                Image infoImage = popupBox.transform.GetChild(2).gameObject.GetComponent<Image>(); // Get info image (3rd child)

                infoTitle.text = selectedAnimal;
                // TODO - set description
                // TODO - set image

                popupBox.SetActive(true);
                meatButton.SetActive(false);
                broccoliButton.SetActive(false);
                Time.timeScale = 0.0f; // Freeze time for the popup
            }
            gm.setInfoDisplay("None"); // Reset the selected animal variable
        }
    }

    /// <summary>
    /// Set the in-game crosshair and indicators based on the current object throw type
    /// </summary>
    void SetThrowIndicators()
    {
        float selectedScale = 1.25f;
        if (throwType == ThrowType.MEAT)
        {
            Cursor.SetCursor(meatCrosshairImage, new Vector2(32f, 32f), CursorMode.Auto);
            meatButton.transform.GetChild(0).gameObject.SetActive(true);
            broccoliButton.transform.GetChild(0).gameObject.SetActive(false);
            meatButton.GetComponent<Image>().rectTransform.localScale = new Vector2(selectedScale, selectedScale);
            broccoliButton.GetComponent<Image>().rectTransform.localScale = new Vector2(1f, 1f);
        }
        else if (throwType == ThrowType.BROCCOLI)
        {
            Cursor.SetCursor(broccoliCrosshairImage, new Vector2(32f, 32f), CursorMode.Auto);
            meatButton.transform.GetChild(0).gameObject.SetActive(false);
            broccoliButton.transform.GetChild(0).gameObject.SetActive(true);
            meatButton.GetComponent<Image>().rectTransform.localScale = new Vector2(1f, 1f);
            broccoliButton.GetComponent<Image>().rectTransform.localScale = new Vector2(selectedScale, selectedScale);
        }
        else
        {
            Cursor.SetCursor(defaultCrosshairImage, new Vector2(32f, 32f), CursorMode.Auto);
            meatButton.transform.GetChild(0).gameObject.SetActive(false);
            broccoliButton.transform.GetChild(0).gameObject.SetActive(false);
            meatButton.GetComponent<Image>().rectTransform.localScale = new Vector2(1f, 1f);
            broccoliButton.GetComponent<Image>().rectTransform.localScale = new Vector2(1f, 1f);
        }
    }

    /// <summary>
    /// Toggle the object throw type between "meat" and "none"
    /// </summary>
    public void ToggleThrowTypeMeat()
    {
        if (throwType == ThrowType.MEAT)
        {
            throwType = ThrowType.NONE;
            throwScript.SetThrowType(ThrowType.NONE);
        }
        else
        {
            throwType = ThrowType.MEAT;
            throwScript.SetThrowType(ThrowType.MEAT);

            SetAssistantText("That's a good chunk of meat!  A carnivorous animal would love it!");
        }
        SetThrowIndicators();
    }

    /// <summary>
    /// Toggle the object throw type between "broccoli" and "none"
    /// </summary>
    public void ToggleThrowTypeBroccoli()
    {
        if (throwType == ThrowType.BROCCOLI)
        {
            throwType = ThrowType.NONE;
            throwScript.SetThrowType(ThrowType.NONE);
        }
        else
        {
            throwType = ThrowType.BROCCOLI;
            throwScript.SetThrowType(ThrowType.BROCCOLI);

            SetAssistantText("That's a fresh veggie!  An herbivorous animal would love that!");
        }
        SetThrowIndicators();
    }

    // Set the text displayed in the assistant text box
    public void SetAssistantText(string text)
    {
        assistantText.text = text;
    }

    // Close the popup box by setting it inactive
    public void ClosePopup()
    {
        popupBox.SetActive(false);
        meatButton.SetActive(true);
        broccoliButton.SetActive(true);
        Time.timeScale = 1.0f; // Unfreeze time when popup is closed
    }

}
