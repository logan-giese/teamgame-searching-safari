using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to manage food throwing behavior based on the position of a mouse click
/// </summary>
public class ThrowScript : MonoBehaviour
{
    // The camera used to detect clicks
    public Camera playerCamera;

    // The prefabs for the throwable objects
    public GameObject meatPrefab;
    public GameObject broccoliPrefab;

    // The distance from the screen to spawn the throwable objects
    public float startDistanceFromScreen = 3.0f;

    // The force at which to throw the object
    public float throwForce = 300.0f;

    // The delay before the player can throw again
    public float delayBetweenThrows = 1.0f;
    private float throwTimer = 0;

    // Whether the throw functionality is enabled (disabled while mouse is over a button)
    private bool throwEnabled = true;

    // The currently selected type of object to throw
    private GameplayUIManager.ThrowType throwType = GameplayUIManager.ThrowType.NONE;

    // The gameplay UI manager (used to send/trigger messages through the UI)
    public GameplayUIManager gameplayUIManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (throwTimer <= 0)
        {
            // Detect click
            if (Input.GetMouseButtonUp(0) && throwType != GameplayUIManager.ThrowType.NONE && throwEnabled)
            {
                // Collect info needed about click position to calculate throw
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = startDistanceFromScreen;
                Vector3 throwPos = playerCamera.ScreenToWorldPoint(mousePos);
                mousePos.z = 100.0f;
                Vector3 lookPos = playerCamera.ScreenToWorldPoint(mousePos);

                // Spawn an appropriate object
                GameObject thrownObject;
                if (throwType == GameplayUIManager.ThrowType.MEAT)
                    thrownObject = Instantiate(meatPrefab, throwPos, transform.rotation);
                else
                    thrownObject = Instantiate(broccoliPrefab, throwPos, transform.rotation);

                // Throw the object (using a temporary empty object to get the angle right)
                GameObject angler = new GameObject("ThrowAngle");
                angler.transform.SetParent(transform);
                angler.transform.LookAt(lookPos);
                thrownObject.GetComponent<Rigidbody>().AddForce(angler.transform.forward * throwForce);
                Destroy(angler);

                // Set a delay before the player can throw again
                throwTimer = delayBetweenThrows;

                // Show a throw message
                gameplayUIManager.SetAssistantText("Good throw!");
                SoundEffectScript.PlayEffect(0);
            }
        }
        else
        {
            throwTimer -= Time.deltaTime;
        }
    }

    public void SetThrowType(GameplayUIManager.ThrowType type)
    {
        throwType = type;
    }

    public void EnableThrow()
    {
        throwEnabled = true;
    }
    public void DisableThrow()
    {
        throwEnabled = false;
    }
}
