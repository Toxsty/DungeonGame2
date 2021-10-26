using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @Author Tobias Haubold
 * @Since 22.09.2021
 * Script für die Steuerung der Kameraperspektive und dazu gehöringen Tasten
 */

public class CameraSwitch : MonoBehaviour
{
    public Transform target;

    Vector3 rightRotation = new Vector3(0f, 1f, 0f);
    Vector3 leftRotation = new Vector3(0f, -1f, 0f);

    char turnDirection = 'r';
    float turnSpeed = 1.0f;
	public float speedMultiplier = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotation changes ( if greater than 360)
        int rotation;
        if (target.eulerAngles.y <= 180f)
        {
            rotation = (int) Math.Round(target.eulerAngles.y);
        }
        else
        {
            rotation = (int) Math.Round(target.eulerAngles.y - 360);
        }

        //turn speed realtive to angle
        turnSpeed = speedMultiplier * (((90 - Math.Abs(Math.Abs(rotation) % 90 - 45)) / 45) - 0.5f) ;

        //cam angles changes on press
        if (Input.GetKeyDown(KeyCode.E))
        {
            target.Rotate(turnSpeed * rightRotation);
            turnDirection = 'l';
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            target.Rotate(turnSpeed * leftRotation);
            turnDirection = 'r';
        }

        //stop if position % 90 = 0
        if (rotation % 90 != 0)
        {
            if (turnDirection == 'l')
            {
                target.Rotate(turnSpeed * rightRotation);
            }
            if(turnDirection == 'r')
            {
                target.Rotate(turnSpeed * leftRotation);
            }
        }
        
    }
}
