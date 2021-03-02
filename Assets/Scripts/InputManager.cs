using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Controller movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        // Take input and pass the appropriate actions to the Controller object.

    }
}
