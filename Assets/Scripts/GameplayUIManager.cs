﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the in-game UI components and relevant variables
/// </summary>
public class GameplayUIManager : MonoBehaviour
{
    // The images to use for in-game crosshairs
    public Texture2D defaultCrosshairImage;
    public Texture2D meatCrosshairImage;
    public Texture2D broccoliCrosshairImage;

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
        // Set the in-game cursor to a crosshair sprite based on the current throw type
        SetCrosshair();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set the in-game crosshair based on the current object throw type
    /// </summary>
    void SetCrosshair()
    {
        if (throwType == ThrowType.MEAT)
            Cursor.SetCursor(meatCrosshairImage, new Vector2(32f, 32f), CursorMode.Auto);
        else if (throwType == ThrowType.BROCCOLI)
            Cursor.SetCursor(broccoliCrosshairImage, new Vector2(32f, 32f), CursorMode.Auto);
        else
            Cursor.SetCursor(defaultCrosshairImage, new Vector2(32f, 32f), CursorMode.Auto);
    }

    /// <summary>
    /// Toggle the object throw type between "meat" and "none"
    /// </summary>
    public void ToggleThrowTypeMeat()
    {
        if (throwType == ThrowType.MEAT)
        {
            throwType = ThrowType.NONE;
        }
        else
        {
            throwType = ThrowType.MEAT;
        }
        SetCrosshair();
    }

    /// <summary>
    /// Toggle the object throw type between "broccoli" and "none"
    /// </summary>
    public void ToggleThrowTypeBroccoli()
    {
        if (throwType == ThrowType.BROCCOLI)
        {
            throwType = ThrowType.NONE;
        }
        else
        {
            throwType = ThrowType.BROCCOLI;
        }
        SetCrosshair();
    }

}
