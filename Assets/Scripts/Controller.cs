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
    private float horizontalSpeed = 10.0f;
    [SerializeField]
    private float minHorizontalSpeed = -15.0f;
    [SerializeField]
    private float maxHorizontalSpeed = 15.0f;
    [SerializeField]
    private float jumpForce = 20.0f;
    [SerializeField]
    private float dashSpeed = 20.0f;
    [SerializeField]
    private float gravity = 4.9f;
    [SerializeField]
    private int numJumps = 2;
    [SerializeField]
    private bool canDash = true;

    [SerializeField]
    private MotionStates current;
    enum MotionStates
    {
        GROUNDED,
        AIRBORNE,
        DASHING,
        WALLCLING
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
    void HorizontalMove(float direction)
    {
        rb.velocity += new Vector2(direction * horizontalSpeed, 0);
        // This provides a maximum horizontal speed while running. Players can achieve faster speeds by dashing, so the clamp should not be universal
        Mathf.Clamp(rb.velocity.x, minHorizontalSpeed, maxHorizontalSpeed);
    }

    /*
     * Adds a burst of momentum on the Y axis. 
     * Players have one ground jump and one air jump
     * Jumps should only activate on a press, so that holding the jump button does not expend an air jump
     */
    void jump()
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
     */
    void dash()
    {

    }


}
