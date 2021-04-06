using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingBook : MonoBehaviour
{
    public int flightSpeed=10, flightDirection=1;
    public float flightTime;
    

    //public Controller player;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb.velocity = new Vector2(flightSpeed, 0) * flightDirection;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = rb.rotation;
        this.rb.rotation-=0.5f;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Controller player = other.gameObject.GetComponent<Controller>();
            player.Hit(flightDirection);
            Destroy(gameObject);
        }
    }

    // Flips flight direction
    public void flipFlight(bool goLeft)
    {
        if (goLeft)
        {
            flightDirection=-1;
        }
    }

    // Takes a time (float) when called and destroys book after set time
    public void setFlightTime(float duration)
    {
        flightTime = duration;
        StartCoroutine(Fly());
    }
    IEnumerator Fly()
    {
        yield return new WaitForSeconds(flightTime);
        Destroy(gameObject);
    }
}
