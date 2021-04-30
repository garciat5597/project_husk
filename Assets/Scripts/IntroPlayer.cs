using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPlayer : MonoBehaviour
{
    float cutsceneTime = 14.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(cutsceneTimer());
    }

    // Update is called once per frame
    void Update()
    {
        // Skip cutscene if we have time
        // Skip button
        if (Input.GetKey(KeyCode.Space))
        {
            StopCoroutine(cutsceneTimer());
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        }
    }

    IEnumerator cutsceneTimer()
    {
        yield return new WaitForSeconds(cutsceneTime);
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
