﻿using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private Button thisButton;
    public string keyData, altKeyData;
    public bool isShift, isSpace, isBackSpace, shiftPressed;

    private void Start()
    {
        if (!isShift && !isSpace && !isBackSpace)
        {
            textDisplay = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(delegate { keyPressed(); });
    }

    public void keyPressed()
    {
        if (!isSpecialKey())
        {
            //input character
            KeyboardHandler.singleton.textEntry(textDisplay.text.ToString());
        } else
        {
            if (isShift)
            {
                if (!shiftPressed)
                {
                    shiftPressed = true;
                } else
                {
                    shiftPressed = false;
                }

                KeyboardHandler.singleton.shiftPressed(shiftPressed);
            }
        }
    }

    private bool isSpecialKey()
    {
        if (!isShift && !isSpace && !isBackSpace)
        {
            return false;
        } else
        {
            return true;
        }
    }
}
