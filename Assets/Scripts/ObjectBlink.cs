using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBlink : MonoBehaviour
{
    public float blinkTimer;
    private float blinkTimerMax;
    private bool isShowing;
    private Image thisImage;

    private void Start()
    {
        thisImage = GetComponent<Image>();
        isShowing = true;
        blinkTimerMax = blinkTimer;
    }

    private void Update()
    {
        if (blinkTimer > 0)
        {
            blinkTimer -= Time.deltaTime;
        } else
        {
            toggleBlink();
        }
    }

    private void toggleBlink()
    {
        if (isShowing)
        {
            isShowing = false;
        }
        else
        {
            isShowing = true;
        }

        thisImage.enabled = isShowing;
        blinkTimer = blinkTimerMax;
    }
}
