using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMovement : MonoBehaviour
{
    public float physicsMovementSpeed = 10;
    public float jumpSpeedForce = 100;
    public float translationMovementSpeed = 1;
    public Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {

    }

    
    // Update is called once per frame
    void Update()
    {
        // Movement using velocity

        float moveX = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(moveX, 0.0f);
        playerRb.position += move * physicsMovementSpeed * Time.deltaTime;

       // playerRb.velocity = new Vector2(0, -9);

        /*
        // Movement using transform
        Vector2 playPos = gameObject.transform.position;
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            //playerRb.velocity = new Vector2(movementSpeed * -1, 0);
            gameObject.transform.position = Vector2.Lerp(playPos, playPos + Vector2.left, translationMovementSpeed / 100);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position = Vector2.Lerp(playPos, playPos + Vector2.right, translationMovementSpeed / 100);
        }
        */
    }
}