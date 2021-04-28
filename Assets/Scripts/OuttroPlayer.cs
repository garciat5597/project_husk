using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OuttroPlayer : MonoBehaviour
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
        
    }

    IEnumerator cutsceneTimer()
    {
        yield return new WaitForSeconds(cutsceneTime);
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
}
