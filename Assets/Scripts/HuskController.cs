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
    float speed = 17f;
    BoxCollider2D collider;
    GameObject player;
    [SerializeField]
    bool canMove = false;
    Vector3 moveTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
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
            direction = direction.normalized;

            transform.position += direction * speed * Time.deltaTime;
        }


    }


    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(spawnTimer);
        // Wake up husk
        collider.enabled = true;
        //canMove = true;
        moveTarget = waypoints.Dequeue();
        Debug.Log("Initial target: " + moveTarget);
    }

    public void addMoveEntry(Vector3 move)
    {
        waypoints.Enqueue(move);
    }

    public void setCanMove(bool val)
    {
        canMove = val;
    }
}
