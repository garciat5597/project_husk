using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script moves a Transform back and forth based on time
public class TimeTestMove : MonoBehaviour
{
    private Transform self;
    private Vector3 v = new Vector3(2, 0, 0);
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // move the Transform based on Sin of time passing
        self.position += v * Mathf.Sin(Time.time) * Time.deltaTime;
        i++;
    }
}
