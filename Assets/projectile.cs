using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bookPrefab;
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

    IEnumerator Shoot()
    {
        reloading = true;
        Instantiate(bookPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSecondsRealtime(reloadTime);
        reloading = false;
    }
}
