using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingBook : MonoBehaviour
{
    public int flightSpeed=10, flightDirection=1;
    public float flightTime;
    public bool flipRotation=false;

    //public Controller player;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb.velocity = new Vector2(flightSpeed, 0) * flightDirection;
        if (flipRotation)
        {
            this.rb.rotation += 10f;
        }
        else
        {
            this.rb.rotation -= 10f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
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
            flipRotation = true;
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
