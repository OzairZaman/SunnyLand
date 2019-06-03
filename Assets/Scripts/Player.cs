using System.Collections;
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
    public float centreRadius = 0.5f;

    public CharacterController2D controller;
    private Vector3 velocity; //
    public Animator anim;
    private bool isClimbing = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, centreRadius);
    }

    private void Reset()
    {
        controller = GetComponent<CharacterController2D>();
    }



    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        
        
        if (!controller.isGrounded && !isClimbing)
        {
            //only happenning while char is in the air
            velocity.y += gravity * Time.deltaTime;
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
            anim.SetFloat("JumpY", velocity.y);

        }

        Climb(inputV, inputH);
        Move(inputH);
        if (!isClimbing)
        {
            controller.move(velocity * Time.deltaTime);
        }
        
    }

    public void Move(float inputH)
    {
        velocity.x = inputH * movementSpeed;
        anim.SetBool("IsRunning", inputH != 0);
        if (inputH != 0)
        {
            GetComponent<SpriteRenderer>().flipX = inputH < 0;
        }
        GetComponent<SpriteRenderer>().flipX = inputH < 0;
    }

    public void Climb(float inputv, float inputH)
    {

        bool isOverLadder = false;
        Vector3 moveDirection = new Vector3(inputH, inputv, 0);

        // get a list of all hit objects overlapping point
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, centreRadius);
        // loop throughall hit objects
        foreach (var hit in hits)
        {
            // check if tagged ladder
            if (hit.tag == "Ladder")
            {
                //player is overlapping ladder 
                isOverLadder = true;
                break; // exits any loop.. ANY!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
        }



        // if the player is overlapping and input vertical is made
        if (isOverLadder && inputv != 0)
        {
            anim.SetBool("IsClimbing", true);
            //      the player is in climbing state
            isClimbing = true;
        }

        #region Part 2 - Translating the Player
        // If player is climbing
        if (isClimbing)
        {
            velocity.y = 0;
            
            // Move player up and down on the ladder (additionally move left and right)
            //transform.Translate(transform.up * inputv * movementSpeed * Time.deltaTime);
            transform.Translate(moveDirection * movementSpeed * Time.deltaTime);
        }
        #endregion

        if (!isOverLadder)
        {
            anim.SetBool("IsClimbing", false);
            isClimbing = false;
        }

        
        anim.SetFloat("ClimbSpeed", moveDirection.magnitude * movementSpeed);
    }

    public void Hurt()
    {

    }

    public void Jump()
    {
        velocity.y = jumpHeight;
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
