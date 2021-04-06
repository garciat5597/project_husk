using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Controller movement;

    float horizMove = 0.0f;
    bool jump = false;
    bool dash = false;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Controller>();

        if (movement)
        {
            Debug.Log("Controller loaded");
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizMove = Input.GetAxisRaw("Horizontal");
        if (horizMove < 0)
        {
            movement.setDirection(-1);
        }
        else if (horizMove > 0)
        {
            movement.setDirection(1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            dash = true;
        }
    }

    private void FixedUpdate()
    {
        movement.HorizontalMove(horizMove);
        if (dash)
        {
            movement.dash();
            dash = false;
        }
        if (jump)
        {
            movement.jump();
            jump = false;
        }
        
    }
}
