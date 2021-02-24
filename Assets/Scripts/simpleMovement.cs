using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMovement : MonoBehaviour
{
    public float physicsMovementSpeed = 10;
    private float translationMovementSpeed = 1;
    public Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {

    }

    
    // Update is called once per frame
    void Update()
    {
    // Movement using velocity
        if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            playerRb.velocity = new Vector2(physicsMovementSpeed * 1, 0);
        }

    // Movement using transform
        Vector2 playPos = gameObject.transform.position;
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            //playerRb.velocity = new Vector2(movementSpeed * -1, 0);
            gameObject.transform.position = Vector2.Lerp(playPos, playPos + Vector2.left, translationMovementSpeed / 100);
        }
    }
}