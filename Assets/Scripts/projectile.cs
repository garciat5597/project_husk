using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform firePoint;
    public flyingBook bookPrefab;
    public GameObject hand;
    public float flightTime = 1.5f;
    public int reloadTime = 1;
    private float handRotationSpeed;
    private bool reloading = false;
    
    public bool flyLeft = false;

    private void Start()
    {
        
        
        reloading = false;
       
    }
    // Update is called once per frame
    void Update()
    {
        float handRotationSpeed = 360 / reloadTime;
        if (flyLeft)
        {
            handRotationSpeed *= -1;
        }
        
        hand.transform.Rotate(new Vector3(0, 0, handRotationSpeed * Time.deltaTime));
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
        projectile.flipFlight(flyLeft);
        projectile.setFlightTime(flightTime);
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
