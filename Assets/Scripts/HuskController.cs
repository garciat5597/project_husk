using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskController : MonoBehaviour
{

    /* Husk controller using new waypoint system 
     * Husk will move to this waypoint at a constant speed matching the player's max run speed
     * 
     */
    float spawnTimer = 5.0f;
    public Queue<Vector3> waypoints = new Queue<Vector3>();
    Rigidbody2D rb;
    // difficulty: master=20 hard=17 normal=15
    float speed = 15f;
    BoxCollider2D collider;
    GameObject player;
    GroundDetection detector;
    Animator anims;
    [SerializeField]
    bool canMove = false;
    Vector3 moveTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        detector = GetComponentInChildren<GroundDetection>();
        // Get player
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (!anims)
        {
            anims = GetComponent<Animator>();
        }
        // Start input collecting as timer rolls down
        waypoints.Enqueue(new Vector3(transform.position.x + 5, transform.position.y + 4, transform.position.z));
        waypoints.Enqueue(new Vector3(transform.position.x + 7, transform.position.y + 2, transform.position.z));
        StartCoroutine(spawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (waypoints.Count > 0)
            {
                // If the husk is close to it's current destination, it picks a new one
                float distanceFromWaypoint = Vector3.Distance(transform.position, moveTarget);
                if (distanceFromWaypoint <= 0.5f)
                {
                    moveTarget = waypoints.Dequeue();
                }


            }

            // Move towards location
            Vector3 direction = new Vector3(moveTarget.x - transform.position.x, moveTarget.y - transform.position.y, 0f);
            // Set animator flags based on next position
            // Flip sprite if the Husk's x scale does not match its next movement direction
            if(direction.magnitude < 0.3)
            {
                // Animator
                if (anims.GetBool("isRunning"))
                {
                    anims.SetBool("isRunning", false);
                }
            }
            else
            {
                // Only bother moving is movement is more significant than half a unit
                if (direction.x > 0 && transform.localScale.x < 0)
                {
                    Flip();
                }
                else if (direction.x < 0 && transform.localScale.x > 0)
                {
                    Flip();
                }
                direction = direction.normalized;

                if (!anims.GetBool("isRunning"))
                {
                    anims.SetBool("isRunning", true);
                }
                if (detector.getGrounded())
                {
                    if (!anims.GetBool("isGrounded"))
                    {
                        anims.SetBool("isGrounded", true);
                    }
                }
                else if (anims.GetBool("isGrounded"))
                {
                    anims.SetBool("isGrounded", false);
                }

                if (direction.y > 0.2)
                {
                    if (!anims.GetBool("isJumping"))
                    {
                        anims.SetBool("isJumping", true);
                    }
                }
                else
                {
                    if (anims.GetBool("isJumping"))
                    {
                        anims.SetBool("isJumping", false);
                    }
                }

                transform.position += direction * speed * Time.deltaTime;
            }
            
        }


    }


    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(spawnTimer);
        // Wake up husk
        //collider.enabled = true;
        canMove = true;
        moveTarget = waypoints.Dequeue();
        Debug.Log("Initial target: " + moveTarget);
        MasterSoundController.StartMainMusic();
    }

    public void addMoveEntry(Vector3 move)
    {
        waypoints.Enqueue(move);
    }

    public void setCanMove(bool val)
    {
        canMove = val;
    }

    void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
