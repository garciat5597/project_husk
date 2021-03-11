using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeScript : MonoBehaviour
{
    private Controller player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if other gameObject's tag is equal to "DeathZone", re-spawn the player to 0,1,0 
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("I'm in");
        }
    }

}
