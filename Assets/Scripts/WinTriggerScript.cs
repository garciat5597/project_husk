using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player enters this trigger, stop gameplay and transition to the win state. Load cutscene and credits, and then return to title menu
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Win trigger tripped");
        }
    }
}
