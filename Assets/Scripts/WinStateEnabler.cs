using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateEnabler : MonoBehaviour
{
    // Drag the game object into this variable in the editor. Since it will start deactivated, we can't get it at runtime in Start
    public GameObject winTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Enable the exit, and then disable itself
            if (winTrigger)
            {
                winTrigger.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
