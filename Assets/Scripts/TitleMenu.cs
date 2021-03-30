using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "DevMicah";
    public GameObject titleMenuUI;

    // Start the game, moving to the first scene
    public void StartGame()
    {
        Debug.Log("Moving to first scene");
        titleMenuUI.SetActive(false);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // Quit out of the game
    public void Quit()
    {
        Debug.Log("*TODO* Quitting the application...");
        Application.Quit();
    }
}
