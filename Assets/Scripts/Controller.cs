using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    /*
     * Final controller work in progress
     */

    [SerializeField]
    private float horizontalSpeed = 10.0f;
    [SerializeField]
    private float jumpForce = 15.0f;
    [SerializeField]
    private float dashSpeed = 15.0f;
    [SerializeField]
    private int numJumps = 2;
    [SerializeField]
    private bool isGrounded = true;
    [SerializeField]
    private bool canDash = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    /*
    * Controls the movement in regards to the X axis.
    * Players can control their movement on this axis at all times
    * There should be a small period of acceleration and deceleration
    */
    void HorizontalMove()
    {

    }

    /*
     * Adds a burst of momentum on the Y axis. 
     * Players have one ground jump and one air jump
     * Jumps should only activate on a press, so that holding the jump button does not expend an air jump
     */
    void jump()
    {

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
