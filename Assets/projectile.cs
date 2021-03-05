using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bookPrefab;
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
        Instantiate(bookPrefab, firePoint.position, firePoint.rotation);
    }
}
