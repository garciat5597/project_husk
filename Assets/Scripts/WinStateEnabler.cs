using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateEnabler : MonoBehaviour
{
    // Drag the game object into this variable in the editor. Since it will start deactivated, we can't get it at runtime in Start
    public GameObject winTrigger;
    public BoxCollider2D winHitbox;
    public GameObject[] toTotem;
    public GameObject[] toWindow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Enable the exit, and then disable itself
            if (winTrigger)
            {
                winHitbox.enabled = true;
                gameObject.SetActive(false);

                // activate and deactivate directions
                for (int temp=0; temp < toTotem.Length; temp++)
                {
                    toTotem[temp].SetActive(false);
                }
                for (int temp = 0; temp < toTotem.Length; temp++)
                {
                    toWindow[temp].SetActive(true);
                }
            }
        }
    }
}
