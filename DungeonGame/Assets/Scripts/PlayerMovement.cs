using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * @Author Tobias Haubold
 * @Since 22.09.2021
 * Script für die Steuerung des Charakters
 */

public class PlayerMovement : MonoBehaviour
{
    public Transform Cam;
    public Transform Player;
    public Transform PlayerCharacter;
    Animator animator;

    Rigidbody rb;

    private Vector3 moveDirection = Vector3.zero;

    public float normalSpeed = 7.0f;
    public float speed = 7.0f;
    public float jumpSpeed = 4.0f;
    public float jumpForce = 7.0f;
    public bool isGrounded = false;
    public float lerpSpeed = 0.05f;
	float timerValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }


	void FixedUpdate()
	{
		RaycastHit hit;

        if (Physics.Raycast(PlayerCharacter.position, Vector3.down, out hit, 2.0f))
		{
            if(hit.distance > 0.5)
			{
				isGrounded = false;
            }
			else
			{
				isGrounded = true;
			}
	    }
    }	


    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");


        //sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10.0f;
            animator.SetFloat("speed", speed/8.0f);
        }
        else
        {
            speed = normalSpeed;
            animator.SetFloat("speed", 1.0f);
        }

        if (!isGrounded)
        {
            animator.SetFloat("speed", 0.0f);
        }


        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && timerValue <= 0)
        {
			timerValue = 1.5f;
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isGrounded = false;
            speed = jumpSpeed;
        }


        //Key changes
        if (isGrounded)
        {
            if (Math.Round(Cam.eulerAngles.y) == 0)
            {
                moveDirection = new Vector3(horizontal, 0.0f, vertical);
            }

            if (Math.Round(Cam.eulerAngles.y) == 90)
            {
                moveDirection = new Vector3(vertical, 0.0f, -horizontal);
            }    

            if (Math.Round(Cam.eulerAngles.y) == 180)
            {
                moveDirection = new Vector3((-horizontal), 0.0f, -vertical);
            }

            if (Math.Round(Cam.eulerAngles.y) == 270)
            {
                moveDirection = new Vector3(-vertical, 0.0f, horizontal);
            }
        }
        


        //actual walking
        Player.Translate(moveDirection.normalized * Time.deltaTime * speed);

        //player rotation
        if (moveDirection != Vector3.zero)
        {
            PlayerCharacter.rotation = Quaternion.Slerp(PlayerCharacter.rotation, Quaternion.LookRotation(moveDirection), lerpSpeed);
        }

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        //emote (H) : T-Pose
        if (Input.GetKey(KeyCode.H))
        {
            animator.SetBool("EmoteT", true);
        }
        else
        {
            animator.SetBool("EmoteT", false);
        }
		
		if(timerValue > 0)
		{
			timerValue -= Time.deltaTime;
		}
    }
}