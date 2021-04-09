using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateEnabler : MonoBehaviour
{
    // Drag the game object into this variable in the editor. Since it will start deactivated we can't get it at runtime in Start
    public GameObject winTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("This should enable the win state trigger");
            if (winTrigger)
            {
                winTrigger.SetActive(true);
            }
        }
    }
}
