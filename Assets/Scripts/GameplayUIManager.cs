using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the in-game UI components and relevant variables
/// </summary>
public class GameplayUIManager : MonoBehaviour
{
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
        // Set the in-game cursor to a crosshair sprite based on the current throw type (and set indicators to disabled)
        SetThrowIndicators();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set the in-game crosshair and indicators based on the current object throw type
    /// </summary>
    void SetThrowIndicators()
    {
        float selectedScale = 1.15f;
        if (throwType == ThrowType.MEAT)
        {
            Cursor.SetCursor(meatCrosshairImage, new Vector2(32f, 32f), CursorMode.Auto);
            meatButton.transform.GetChild(1).gameObject.SetActive(true);
            broccoliButton.transform.GetChild(1).gameObject.SetActive(false);
            meatButton.GetComponent<Image>().rectTransform.localScale = new Vector2(selectedScale, selectedScale);
            broccoliButton.GetComponent<Image>().rectTransform.localScale = new Vector2(1f, 1f);
        }
        else if (throwType == ThrowType.BROCCOLI)
        {
            Cursor.SetCursor(broccoliCrosshairImage, new Vector2(32f, 32f), CursorMode.Auto);
            meatButton.transform.GetChild(1).gameObject.SetActive(false);
            broccoliButton.transform.GetChild(1).gameObject.SetActive(true);
            meatButton.GetComponent<Image>().rectTransform.localScale = new Vector2(1f, 1f);
            broccoliButton.GetComponent<Image>().rectTransform.localScale = new Vector2(selectedScale, selectedScale);
        }
        else
        {
            Cursor.SetCursor(defaultCrosshairImage, new Vector2(32f, 32f), CursorMode.Auto);
            meatButton.transform.GetChild(1).gameObject.SetActive(false);
            broccoliButton.transform.GetChild(1).gameObject.SetActive(false);
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

}
