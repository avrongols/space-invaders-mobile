using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
        PlayerPrefManager.ResetPlayerScore();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        PlayerPrefManager.ResetPlayerScore();
    }

    public void QuitGame()
    {
        Application.Quit();
        PlayerPrefManager.ResetPlayerScore();
    }
}
