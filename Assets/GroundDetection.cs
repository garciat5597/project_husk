using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    private bool onGround = false;
    
    // Collision handler
    private void OnCollisionStay2D(Collision2D collision)
    {
        // When touching the floor
        if (collision.gameObject.tag == "Floor")
        {
            // Become grounded, refresh jumps.
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When touching the floor
        if (collision.gameObject.tag == "Floor")
        {
            // Become grounded, refresh jumps.
            onGround = false;
        }
    }

    public bool getGrounded()
    {
        return onGround;
    }
}
