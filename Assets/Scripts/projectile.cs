using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform firePoint;
    public flyingBook[] bookPrefab;

    /*public flyingBook bookPrefab1;
    public flyingBook bookPrefab2;
    public flyingBook bookPrefab3;
    public flyingBook bookPrefab4;
    public flyingBook bookPrefab5;*/

    public GameObject hand;
    public float flightTime = 1.5f;
    public int reloadTime = 1;
    private float handRotationSpeed;
    private bool reloading = false;
    public bool flyLeft = false;
    private int bookLength;
    private int bookSpot=0;

    private void Start()
    {
        reloading = false;
        bookLength = bookPrefab.Length;
    }
    // Update is called once per frame
    void Update()
    {
        float handRotationSpeed = 360 / reloadTime;
        handRotationSpeed *= -1;
        
        
        hand.transform.Rotate(new Vector3(0, 0, handRotationSpeed * Time.deltaTime));
        if (reloading == false)
        {
            StartCoroutine(Shoot());
        }
    }

    // When called: activates reload, makes an instance of a book, set's it's flight time, and then waits till the reload time is finished before deactivating reload
    IEnumerator Shoot()
    {
        // Play throw sound
        MiscEnemySoundController.PlayBookThrow(hand);

        bookSpot++;
        if (bookSpot >= bookLength)
        {
            bookSpot = 0;
        }

        reloading = true;
        var projectile = Instantiate(bookPrefab[bookSpot], firePoint.position, firePoint.rotation);
        projectile.flipFlight(flyLeft);
        projectile.setFlightTime(flightTime);
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
