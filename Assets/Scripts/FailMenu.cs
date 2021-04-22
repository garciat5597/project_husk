using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailMenu : MonoBehaviour
{
    public static bool Failed = false;
    public Controller playerController;
    public GameObject failMenuUI;
    public Animator transition;

    private void Start()
    {
        if (!playerController)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.getDead())
        {
            activateFailScreen();
        }
    }

    // Pause the game, setting timescale to 0, activate the fail menu
    void activateFailScreen()
    {
        failMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        Failed = true;
    }

    // Restart the level
    public void Restart()
    {
        failMenuUI.SetActive(false);
        Failed = false;
        //Time.timeScale = 1f;
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        // resume: timescale to 1, unpause, etc
        // reset player position
        // reset shadow position
        // reset enemies and traps
    }

    // return to the main menu
    public void Quit()
    {
        failMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        Failed = false;
        SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
    }

    void transitionOut()
    {
        transition.SetTrigger("Start");
    }
}
