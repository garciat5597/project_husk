using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingBook : MonoBehaviour
{
    public int flightSpeed=10;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(flightSpeed, 0);

    }
    /*// Update is called once per frame
    void Update()
    {
        rb.rotation+=1;
    }*/
}
