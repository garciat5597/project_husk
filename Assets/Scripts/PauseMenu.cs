using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject controlsMenu;

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
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
        MasterSoundController.PauseSFX();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    // Resume the game
    public void Resume ()
    {
        MasterSoundController.ResumeSFX();
        Debug.Log("Resuming");
        pauseMenuUI.SetActive(false);
        if (controlsMenu.activeSelf)
        {
            controlsMenu.SetActive(false);
        }
        Time.timeScale = 1f;
        IsPaused = false;
    }

    // Restart the level
    public void Restart ()
    {
        MasterSoundController.StopAllSFX();
        MasterSoundController.ResumeSFX();
        Debug.Log("*TODO* Restarting Level...");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        MasterSoundController.StopMusic();
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        // resume: timescale to 1, unpause, etc
        // reset player position
        // reset shadow position
        // reset enemies and traps
    }

    public void ControlsMenu()
    {
        pauseMenuUI.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        MasterSoundController.StopAllSFX();
        MasterSoundController.ResumeSFX();
        pauseMenuUI.SetActive(true);
        controlsMenu.SetActive(false);
    }

    // return to the main menu
    public void Quit()
    {
        Debug.Log("*TODO* Quitting to Menu...");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        MasterSoundController.StopMusic();
        MasterSoundController.StopAllSFX();
        MasterSoundController.ResumeSFX();
        SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
    }
}
