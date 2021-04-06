using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform firePoint;
    public flyingBook bookPrefab;
    public float flightTime = 1.5f;
    public int reloadTime = 1;
    private bool reloading = false;

    private void Start()
    {
        reloading = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (reloading == false)
        {
            StartCoroutine(Shoot());
        }
    }

    // When called: activates reload, makes an instance of a book, set's it's flight time, and then waits till the reload time is finished before deactivating reload
    IEnumerator Shoot()
    {
        reloading = true;
        var projectile = Instantiate(bookPrefab, firePoint.position, firePoint.rotation);
        projectile.setFlightTime(flightTime);
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
