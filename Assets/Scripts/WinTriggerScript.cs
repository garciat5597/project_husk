using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTriggerScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player enters this trigger, stop gameplay and transition to the win state. Load cutscene and credits, and then return to title menu
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Win trigger tripped");

            //pause
            Time.timeScale = 0f;

            StartCoroutine(fadeOut());
        }

        IEnumerator fadeOut()
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene("Credits", LoadSceneMode.Single);
        }
    }
}
