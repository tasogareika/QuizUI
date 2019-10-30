using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    private Transform thisTransform;
    public float rotateMax, rotateSpeed, resetTimer;
    private float resetTimerMax;
    private bool moveBack, canMove;

    private void Start()
    {
        thisTransform = this.transform;
        moveBack = false;
        canMove = true;
        resetTimerMax = resetTimer;
        thisTransform.eulerAngles = Vector3.zero;
    }

    private void Update()
    {
        Vector3 rot = thisTransform.eulerAngles;

        if (canMove)
        {
            if (!moveBack)
            {
                if (rot.z < rotateMax)
                {
                    thisTransform.eulerAngles = new Vector3(rot.x, rot.y, rot.z + rotateSpeed);
                }
                else
                {
                    moveBack = true;
                }
            }
            else
            {
                if (rot.z > 1)
                {
                    thisTransform.eulerAngles = new Vector3(rot.x, rot.y, rot.z - rotateSpeed);
                }
                else
                {
                    moveBack = false;
                    canMove = false;
                }
            }
        } else
        {
            if (resetTimer > 0)
            {
                resetTimer -= Time.deltaTime;
            } else
            {
                resetTimer = resetTimerMax;
                canMove = true;
            }
        }
    }
}
