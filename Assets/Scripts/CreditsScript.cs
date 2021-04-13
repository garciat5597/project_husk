using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    float creditsTimer;
    public GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        creditsTimer = 20.0f;
        StartCoroutine(Expiration());
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

    IEnumerator Expiration()
    {
        yield return new WaitForSeconds(creditsTimer);
        SceneManager.LoadScene("TitleMenu");
    }
}
