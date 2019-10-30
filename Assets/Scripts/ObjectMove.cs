using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public float moveSpeedX, moveSpeedY, moveTimer;
    private float moveTimerMax;
    private Vector2 startPos;
    private RectTransform thisRect;

    private void Start()
    {
        thisRect = GetComponent<RectTransform>();
        moveTimerMax = moveTimer;
        startPos = thisRect.anchoredPosition;
    }

    private void Update()
    {
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
            Vector2 lastPos = thisRect.anchoredPosition;
            Vector2 currPos = new Vector2(lastPos.x + moveSpeedX, lastPos.y + moveSpeedY);
            thisRect.anchoredPosition = currPos;
        } else
        {
            moveTimer = moveTimerMax;
            thisRect.anchoredPosition = startPos;
        }
    }
}
