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

    [SerializeField]
    private float horizontalSpeed = 30.0f;
    [SerializeField]
    private Vector2 currentVelocity = Vector2.zero;
    [SerializeField]
    private float jumpForce = 15000.0f;
    [SerializeField]
    private float dashSpeed = 8000.0f;
    [SerializeField]
    private float gravity = 9.8f;
    [SerializeField]
    private int numJumps = 2;
    [SerializeField]
    private static float MAX_FALL = -15.0f;
    [SerializeField]
    private static int MAX_JUMPS = 2;
    [SerializeField]
    private bool canDash = true;

    [SerializeField]
    private MotionStates current;
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

        current = MotionStates.GROUNDED;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Physics logic should be updated here
    private void FixedUpdate()
    {
        // Update velocity based on player state and motion
        
    }

    /*
    * Controls the movement in regards to the X axis.
    * Players can control their movement on this axis at all times
    * There should be a small period of acceleration and deceleration
    */
    public void HorizontalMove(float direction)
    {
        Vector2 targetVel = new Vector2(direction * horizontalSpeed, 0f);
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
            rb.AddForce(new Vector2(0, jumpForce));
            numJumps--;
            // Update the state machine if needed
            if (current != MotionStates.AIRBORNE)
            {
                current = MotionStates.AIRBORNE;
            }
        }
    }

    /*
     * Adds a burst of momentum on the X axis.
     * Player recieves a flat number addeded to their momentum in the direction they are facing, after which it falls back down to the normal horizontal movement speed
     * Works on both ground and in air, but will have a different animation in the air.
     * For the length of the dash animation, Y momentum is halted.
     */
    public void dash(int sign)
    {
        if (canDash)
        {
            // Change this so that accounts for player direction
            Vector2 dashForce = new Vector2(sign * dashSpeed, 0);
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

    IEnumerator dashGravity()
    {
        yield return new WaitForSeconds(0.45f);
        rb.gravityScale = gravity;
    }

    // Collision handler
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision detected");
        if (collision.gameObject.tag == "Floor")
        {
            Debug.Log("floor detected");
            // Become grounded, refresh jumps.
            current = MotionStates.GROUNDED;
            numJumps = MAX_JUMPS;
        }
    }
}
