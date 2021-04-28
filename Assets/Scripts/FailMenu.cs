using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailMenu : MonoBehaviour
{
    public static bool Failed = false;
    public Controller playerController;
    public GameObject failMenuUI;
    public GameObject foreground;
    Image fgImage;
    public GameObject background;
    public Animator transition;
    public Text failTitle;

    private string[] textOptions = new string[] {"The Doctor is Out", "You Died", "Your Past Caught Up", "Try Again?" };
    string currentString;

    private void Start()
    {
        Failed = false;
        currentString = textOptions[0];
        if (!playerController)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        }
        fgImage = foreground.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.getDead() && !Failed)
        {
            Debug.Log("Start death screen");
            activateFailScreen();
        }


    }

    // Pause the game, setting timescale to 0, activate the fail menu
    void activateFailScreen()
    {
        // Choose a random text for the title
        int random = Random.Range(0, 4);
        currentString = textOptions[random];
        failTitle.text = currentString;

        // Stop SFX
        MasterSoundController.StopAllSFX();

        background.SetActive(true);
        foreground.SetActive(true);
        Failed = true;
        StartCoroutine(failMenuDelay());
    }

    IEnumerator failMenuDelay()
    {
        // Time before the menu is active
        yield return new WaitForSeconds(3.0f);
        foreground.SetActive(false);
        failMenuUI.SetActive(true);
    }

    // Restart the level
    public void Restart()
    {
        // Stop SFX
        MasterSoundController.StopAllSFX();

        StopAllCoroutines();
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        foreground.SetActive(false);
        failMenuUI.SetActive(false);
        Failed = false;
        //Time.timeScale = 1f;
        // resume: timescale to 1, unpause, etc
        // reset player position
        // reset shadow position
        // reset enemies and traps
    }

    // return to the main menu
    public void Quit()
    {
        // Stop SFX
        MasterSoundController.StopAllSFX();

        StopAllCoroutines();
        SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
        foreground.SetActive(false);
        failMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        Failed = false;
        
    }

    void transitionOut()
    {
        transition.SetTrigger("Start");
    }



}
