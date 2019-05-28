﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed = 10f;
    public float gravity = -20f;
    public float jumpHeight = 8f;

    public CharacterController2D controller;
    private Vector3 motion; //
    public Animator anim;
   
    private void Reset()
    {
        controller = GetComponent<CharacterController2D>();
    }



    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        
        
        if (!controller.isGrounded)
        {
            //only happenning while char is in the air
            motion.y += gravity * Time.deltaTime;
        }
        else
        {
            //we are here constantly while grounded
            if (Input.GetButtonDown("Jump"))
            {
                //we are here for only one update cycle as we call Jump() 
                Jump();

                //we are in air and we set IsJumping to true
                //since char is in the air (and while in the air) IsJumping will remain true (till we are grounded)
                anim.SetBool("IsJumping", true);
            }
            else
            {
                //constantly happening while on ground 
                anim.SetBool("IsJumping", false);
            }
        }


        //constatnly being checked every update cycle
        if (anim.GetBool("IsJumping"))
        {
            anim.SetFloat("JumpY", motion.y);

        }

        Climb(inputV);
        Move(inputH);
        controller.move(motion * Time.deltaTime);
    }

    public void Move(float inputH)
    {
        motion.x = inputH * movementSpeed;
        anim.SetBool("IsRunning", inputH != 0);
        GetComponent<SpriteRenderer>().flipX = inputH < 0;
    }

    public void Climb(float inputv)
    {

    }

    public void Hurt()
    {

    }

    public void Jump()
    {
        motion.y = jumpHeight;
    }
}


/*
 * 
 * Manny's Code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

public class Player : MonoBehaviour
{
  public float gravity = -10, moveSpeed = 10f, jumpHeight = 8f;

  private CharacterController2D controller;
  private SpriteRenderer rend;
  private Animator anim;

  private Vector3 motion;

  void Start()
  {
    controller = GetComponent<CharacterController2D>();
    rend = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
  }

  void Update()
  {
    float inputH = Input.GetAxis("Horizontal");
    float inputV = Input.GetAxis("Vertical");
    // If character is grounded
    if (!controller.isGrounded)
    {
      // Apply gravity
      motion.y += gravity * Time.deltaTime;
    }
    // If space is pressed
    if (Input.GetButtonDown("Jump"))
    {
      // Make the player jump
      Jump();
    }
    // Climb up or down depending on Y value
    Climb(inputV);
    // Move left or right depending on X value
    Move(inputH);
    // Move the controller with modified motion
    controller.move(motion * Time.deltaTime);
  }

  public void Move(float inputH)
  {
    motion.x = inputH * moveSpeed;
    anim.SetBool("IsRunning", inputH != 0);
    rend.flipX = inputH < 0;
  }

  public void Climb(float inputV)
  {

  }

  public void Hurt()
  {

  }

  public void Jump()
  {
    motion.y = jumpHeight;
  }
}

 * 
 */
