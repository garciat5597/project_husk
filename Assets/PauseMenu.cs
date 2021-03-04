using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Pause the game, setting timescale to 0
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    // Resume the game
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    // Restart the level
    public void Restart ()
    {
        Debug.Log("*TODO* Restarting Level...");
        // resume: timescale to 1, unpause, etc
        // reset player position
        // reset shadow position
        // reset enemies and traps
    }

    // return to the main menu
    public void Quit()
    {
        Debug.Log("*TODO* Quitting to Menu...");
        // change to menu scene
    }
}
