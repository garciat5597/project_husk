using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    /*
     * Final controller work in progress
     * Will be used for both the player and Husk
     */
    private Rigidbody2D rb;

    
    public float horizontalSpeed = 30.0f;
    [SerializeField]
    private Vector2 currentVelocity = Vector2.zero;
    [SerializeField]
    private float jumpForce = 1500.0f;
    [SerializeField]
    private float dashSpeed = 1500.0f;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private int numJumps = 2;
    [SerializeField]
    private static float MAX_FALL = -25.0f;
    [SerializeField]
    private static int MAX_JUMPS = 2;
    [SerializeField]
    private bool canDash = true;
    [SerializeField]
    private int direction = 1;

    [SerializeField]
    private MotionStates currentState;
    enum MotionStates
    {
        GROUNDED,
        AIRBORNE,
        DASHING,
        WALLCLING,
        STUNNED
    }

    // Start is called before the first frame update
    void Start()
    {
        // Load the character's rigidbody
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        gravity = rb.gravityScale;
        currentState = MotionStates.GROUNDED;

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Physics logic should be updated here
    private void FixedUpdate()
    {
        // Clamp fall speed
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, MAX_FALL));
        
    }

    public void setDirection(int nDirection)
    {
        direction = nDirection;
    }

    /*
    * Controls the movement in regards to the X axis.
    * Players can control their movement on this axis at all times
    * There should be a small period of acceleration and deceleration
    */
    public void HorizontalMove(float horizMove)
    {
        Vector2 targetVel = new Vector2(horizMove * horizontalSpeed, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVel, ref currentVelocity, 0.3f);
    }

    /*
     * Adds a burst of momentum on the Y axis. 
     * Players have one ground jump and one air jump
     * Jumps should only activate on a press, so that holding the jump button does not expend an air jump
     */
    public void jump()
    {
        if (numJumps > 0)
        {
            if (currentState == MotionStates.WALLCLING)
            {
                // Special jump arc out of wallcling
                rb.AddForce(new Vector2(-direction * (jumpForce / 2), jumpForce));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0, jumpForce));
            }
            numJumps--;
            // Update the state machine if needed
            if (currentState != MotionStates.AIRBORNE)
            {
                currentState = MotionStates.AIRBORNE;
            }
        }
    }

    /*
     * Adds a burst of momentum on the X axis.
     * Player recieves a flat number addeded to their momentum in the direction they are facing, after which it falls back down to the normal horizontal movement speed
     * Works on both ground and in air, but will have a different animation in the air.
     * For the length of the dash animation, Y momentum is halted.
     */
    public void dash()
    {
        if (canDash)
        {
            // Change this so that accounts for player direction
            Vector2 dashForce = new Vector2(direction * dashSpeed, 0);
            // Pause the player at their current y axis value
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0f;
            rb.AddForce(dashForce);
            canDash = false;
            StartCoroutine(dashCooldown());
            StartCoroutine(dashGravity());
            // Change state to dashing
        }
    }

    // Dash currently working on a 1 second cooldown.
    IEnumerator dashCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        canDash = true;
    }

    // Wait to reset gravity
    IEnumerator dashGravity()
    {
        yield return new WaitForSeconds(0.45f);
        // Prevent gravity from re-enabling early if player dashes into a wall.
        if(currentState != MotionStates.WALLCLING)
        {
            rb.gravityScale = gravity;
        }
    }

    // Collision handler
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When touching the floor
        if (collision.gameObject.tag == "Floor")
        {
            // Become grounded, refresh jumps.
            currentState = MotionStates.GROUNDED;
            numJumps = MAX_JUMPS;
        }
        // Activate wallcling
        if (collision.gameObject.tag == "Wall")
        {
            currentState = MotionStates.WALLCLING;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0f;
            // Recover one jump if the player has none when they wallcling
            if (numJumps < 1)
            {
                numJumps++;
            }
            StartCoroutine(wallclingGravity());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When leaving the ground, check if jumps > 1. If so, reduce to 1
        if (collision.gameObject.tag == "Floor")
        {
            currentState = MotionStates.AIRBORNE;
            if (numJumps > 1)
            {
                numJumps--;
            }
        }
        // Reset gravity and exit wallcling prematurely
        if (collision.gameObject.tag == "Wall")
        {
            currentState = MotionStates.AIRBORNE;
            rb.gravityScale = gravity;
        }
    }

    IEnumerator wallclingGravity()
    {
        // Max time that player can hold a wallcling.
        yield return new WaitForSeconds(1.0f);
        currentState = MotionStates.AIRBORNE;
        rb.gravityScale = gravity;
    }
}
