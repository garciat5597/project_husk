using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskController : MonoBehaviour
{
    float spawnTimer = 5.0f;
    Queue<InputLog> inputs = new Queue<InputLog>();
    Rigidbody2D rb;
    //SpriteRenderer sprite;
    BoxCollider2D collider;
    GameObject player;
    [SerializeField]
    bool canMove = false;
    Controller movement;

    // Movement flags
    float horizMove = 0.0f;
    bool jump = false;
    bool dash = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        if (!movement)
        {
            movement = GetComponent<Controller>();
        }
        // Get player
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        // Start input collecting as timer rolls down
        StartCoroutine(spawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        // If husk is allowed to move, start pulling from the queue
        if (canMove)
        {
            // Check that the Husk queue is not empty
            // TODO: Check that the player is not stunned
            // TODO: If the player is still or stunned, continue the Husk forward towards them
            if (inputs.Count > 0)
            {

                // Raise movement flags based on dequeued input
                InputLog currentInputs = inputs.Dequeue();
                if (currentInputs.directionalInput < 0)
                {
                    movement.setDirection(-1);
                    horizMove = currentInputs.directionalInput;
                }
                else if (currentInputs.directionalInput > 0)
                {
                    movement.setDirection(1);
                    horizMove = currentInputs.directionalInput;
                }

                if (currentInputs.jumpPressed)
                {
                    jump = true;
                }

                if (currentInputs.dashPressed)
                {
                    dash = true;
                }
            }
            
        }
    }

    private void FixedUpdate()
    {
        // Update in the controller
        movement.HorizontalMove(horizMove);
        horizMove = 0.0f;
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

    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(spawnTimer);
        // Wake up husk
        collider.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //sprite.enabled = true;
        canMove = true;
    }

    public void addQueueEntry(InputLog nInput)
    {
        inputs.Enqueue(nInput);
    }
}
