using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{

    public GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Roll credits
        if (credits)
        {
            credits.transform.position += new Vector3(0, 0.1f, 0);
        }
    }
}
