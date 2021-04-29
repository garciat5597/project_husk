using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    /*
     * Final controller object
     * Used to control the player's movement
     */
    private Rigidbody2D rb;
    HuskController husk;
    GroundDetection detector;
    Animator anims;
    SpriteRenderer sprite;

    public float horizontalSpeed = 25.0f;
    [SerializeField]
    private Vector2 currentVelocity = Vector2.zero;
    [SerializeField]
    private float jumpForce = 1200.0f;
    [SerializeField]
    private float dashSpeed = 1000.0f;
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
    bool addEntry = true;
    Vector2 knockback;
    bool isDead = false;
    bool canMoveHoriz = true;

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
        isDead = false;
        // Find the Husk
        husk = GameObject.FindGameObjectWithTag("Husk").GetComponent<HuskController>();
        // Load the ground detector component
        detector = GetComponentInChildren<GroundDetection>();
        // Load the character's rigidbody
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (!anims)
        {
            anims = GetComponent<Animator>();
        }

        gravity = rb.gravityScale;
        currentState = MotionStates.GROUNDED;

    }

    /*
     * Clamp's the player's y velocity and checks if a new waypoint should be sent to the Husk
     */
    private void FixedUpdate()
    {
        // Update Y position for ambiance
        MasterSoundController.UpdatePlayerPosition(transform.position.y);

        if (!detector.getGrounded())
        {
            currentState = MotionStates.AIRBORNE;
        }
        // Clamp fall speed
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, MAX_FALL));
        // Create a new entry every second
        if (addEntry)
        {
            husk.addMoveEntry(gameObject.transform.position);
            StartCoroutine(addHuskWaypoint());
        }

        if (detector.getGrounded())
        {
            // Become grounded, refresh jumps.
            currentState = MotionStates.GROUNDED;
            numJumps = MAX_JUMPS;
        }

        if (direction > 0 && transform.localScale.x < 0)
        {
            flipX();
        }
        else if (direction < 0 && transform.localScale.x > 0)
        {
            flipX();
        }

        // Adjust animator bols based on motion state
        switch (currentState)
        {
            case MotionStates.GROUNDED:
                // If player is grounded, anim state should be either idle or running 
                if (!anims.GetBool("isGrounded"))
                {
                    anims.SetBool("isGrounded", true);
                    anims.SetBool("isOnWall", false);
                }
                break;
            case MotionStates.AIRBORNE:
                // If state is airborne, anim state should be falling unless jumping or dashing (set by trigger)
                if (anims.GetBool("isGrounded"))
                {
                    anims.SetBool("isGrounded", false);
                    anims.SetBool("isOnWall", false);
                }
                break;
            case MotionStates.WALLCLING:
                // If state is wallcling, anim state should be wallcling
                if (!anims.GetBool("isOnWall")){
                    anims.SetBool("isOnWall", true);
                }
                break;
            default:
                // Unknown case
                break;

        }

        // Visualize velocity
        if(rb.velocity.magnitude != 0)
        {
            Debug.DrawRay(transform.position, rb.velocity.normalized * 5, Color.blue);
        }
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
        if (currentState != MotionStates.STUNNED && canMoveHoriz)
        {
            Vector2 targetVel = new Vector2(horizMove * horizontalSpeed, rb.velocity.y);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVel, ref currentVelocity, 0.3f);

            if (!anims.GetBool("isRunning") && horizMove != 0)
            {
                anims.SetBool("isRunning", true);
            }
            else if (anims.GetBool("isRunning") && horizMove == 0)
            {
                anims.SetBool("isRunning", false);
            }
        }
        

    }

    /*
     * Adds a burst of momentum on the Y axis. 
     * Players have one ground jump and one air jump
     * Jumps should only activate on a press, so that holding the jump button does not expend an air jump
     */
    public void jump()
    {
        if (numJumps > 0 && currentState != MotionStates.STUNNED)
        {
            if (currentState == MotionStates.WALLCLING)
            {
                anims.SetBool("isOnWall", false);
                canMoveHoriz = false;
                StartCoroutine(postWallclingTimer());
                StopCoroutine("wallclingGravity");
                // Special jump arc out of wallcling
                rb.AddForce(new Vector2(-direction * (jumpForce * 0.8f), jumpForce));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0, jumpForce));
            }
            numJumps--;
            anims.SetTrigger("Jump");

            // Play jump sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Doctor/Jump");

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
     * Works on both ground and in air
     * For the length of the dash animation, Y momentum is halted.
     */
    public void dash()
    {
        if (canDash && currentState != MotionStates.STUNNED)
        {
            // Change this so that accounts for player direction
            Vector2 dashForce = new Vector2(direction * dashSpeed, 0);
            // Pause the player at their current y axis value
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0f;
            // ensure wallcling state is exited on dash, since there is no dash state
            if (currentState == MotionStates.WALLCLING)
            {
                currentState = MotionStates.AIRBORNE;
            }
            anims.SetTrigger("Dash");
            rb.AddForce(dashForce);
            canDash = false;
            StartCoroutine(dashCooldown());
            StartCoroutine(dashGravity());
        }
    }

    /*
     * Dash cooldown timer
     */
    IEnumerator dashCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        canDash = true;
    }

    /*
     * Prevents gravity from taking effect during a dash animation
     */
    IEnumerator dashGravity()
    {
        yield return new WaitForSeconds(0.45f);
        // Prevent gravity from re-enabling early if player dashes into a wall.
        if(currentState != MotionStates.WALLCLING)
        {
            rb.gravityScale = gravity;
        }
    }


    /*
     * Sends a new waypoint to the Husk every 0.1 second
     */
    IEnumerator addHuskWaypoint()
    {
        addEntry = false;
        yield return new WaitForSeconds(0.10f);
        addEntry = true;
    }

    /*
     * Activates wallcling on collision with a wall object
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Activate wallcling
        if (collision.gameObject.tag == "Wall")
        {
            currentState = MotionStates.WALLCLING;
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0f;
            // Recover one jump if the player has none when they wallcling
            if (numJumps < 1)
            {
                numJumps++;
            }
            StartCoroutine("wallclingGravity");
        }
    }

    /*
     * Trigger that is called when the player comes in contact with the Husk
     * Activates death variable to kill player controls and end the game
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Husk")
        {
            // Die
            isDead = true;
        }
    }


    /*
     * Updates the player states when exiting collissions with walls and floors
     */
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
            anims.SetBool("isOnWall", false);
            rb.gravityScale = gravity;
        }
    }


    /*
     * Timer that controls how long a player can hold onto a wall
     * Upon expiration, reactiviates gravity and updates the player state
     * Can be exited early with a walljump
     */
    IEnumerator wallclingGravity()
    {
        // Max time that player can hold a wallcling.
        yield return new WaitForSeconds(1.5f);
        currentState = MotionStates.AIRBORNE;
        rb.gravityScale = gravity;
    }

    /*
     * Imparts force and stuns the Doctor when they are hit by a book
     * Stun length: 1.5s or until the Doctor hits the floor
     */
    public void Hit(int projectileDirection)
    {
        rb.velocity = Vector2.zero;
        knockback = new Vector2(projectileDirection * 1000, 500);
        rb.AddForce(knockback);
        currentState = MotionStates.STUNNED;
    }

    /*
     * Prevents the player's input from affecting the walljump trajectory for a fraction of a second
     * Done to make the wall jump more consistent
     */
    IEnumerator postWallclingTimer()
    {
        yield return new WaitForSeconds(0.2f);
        canMoveHoriz = true;

    }

    /*
     * Controls how long the player is stunned after being hit by a thrower
     */
    IEnumerator stunTimer()
    {
        yield return new WaitForSeconds(1.5f);
        currentState = MotionStates.AIRBORNE;
    }

    /*
     * public getter for finding out the state of the player
     */
    public bool getDead()
    {
        return isDead;
    }

    private void flipX()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
