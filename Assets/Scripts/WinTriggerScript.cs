using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTriggerScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;
    public HuskController husk;
    [SerializeField]
    practice practiceMode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player enters this trigger, stop gameplay and transition to the win state. Load cutscene and credits, and then return to title menu
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Win trigger tripped");

            //pause the husk
            husk.setCanMove(false);

            //fades out from level to credits
            StartCoroutine(fadeOut());
        }

        IEnumerator fadeOut()
        {
            
            MasterSoundController.StopAllSFX();
            MasterSoundController.StopMusic();
            if (practiceMode.getGameMode())
            {
                transition.SetTrigger("Start");
                yield return new WaitForSeconds(transitionTime);
                SceneManager.LoadScene("EndCutscene", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
            }
        }
    }
}
