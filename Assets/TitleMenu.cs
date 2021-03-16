using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public GameObject titleMenuUI;

    // Start the game, moving to the first scene
    public void StartGame()
    {
        Debug.Log("Moving to first scene");
        titleMenuUI.SetActive(false);
        SceneManager.LoadScene("DevMicah", LoadSceneMode.Single);
    }

    // Quit out of the game
    public void Quit()
    {
        Debug.Log("*TODO* Quitting the application...");
        Application.Quit();
    }
}
