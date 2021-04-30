using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public GameObject titleMenuUI;
    public GameObject controlsMenu;
    public practice practiceChecker;

    [SerializeField] private string firstLevel = "IntroCutscene";

    // Start the game, moving to the first scene
    public void StartGame()
    {
        Debug.Log("Moving to first scene");
        titleMenuUI.SetActive(false);
        practiceChecker.setPracticeMode(false);
        SceneManager.LoadScene(firstLevel, LoadSceneMode.Single);
    }

    public void PracticeGame()
    {
        Debug.Log("Moving to practice");
        titleMenuUI.SetActive(false);
        practiceChecker.setPracticeMode(true);
        SceneManager.LoadScene(firstLevel, LoadSceneMode.Single);
    }

    public void ControlsMenu()
    {
        titleMenuUI.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        titleMenuUI.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    // Quit out of the game
    public void Quit()
    {
        Debug.Log("*TODO* Quitting the application...");
        Application.Quit();
    }
}
