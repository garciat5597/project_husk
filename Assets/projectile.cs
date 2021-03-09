using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform firePoint;
    private bool reloading = false;

    private void Start()
    {
        reloading = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (reloading == false){
            Shoot();
        }
    }

    void Shoot()
    {

    }
}
