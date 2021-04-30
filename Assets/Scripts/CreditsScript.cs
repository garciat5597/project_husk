using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    float creditsTimer;
    public GameObject credits;

    public Text[] creditsTextBlocks;
    // Start is called before the first frame update
    void Start()
    {
        creditsTextBlocks = GameObject.Find("CreditsText").GetComponentsInChildren<Text>();
        creditsTimer = 10.0f;
        StartCoroutine(Expiration());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Roll credits
        if (credits)
        {
            TextFadeIn();
        }

        // Skip button
        if (Input.GetKey(KeyCode.Space))
        {
            StopCoroutine(Expiration());
            SceneManager.LoadScene("TitleMenu");
        }
    }

    void TextFadeIn()
    {
        foreach (Text block in creditsTextBlocks)
        {
            if (block.color.a < 1.0f)
            {
                block.color = new Color(block.color.r, block.color.g, block.color.b, block.color.a + (Time.deltaTime));
            }
        }
    }

    IEnumerator Expiration()
    {
        yield return new WaitForSeconds(creditsTimer);
        SceneManager.LoadScene("TitleMenu");
    }
}
