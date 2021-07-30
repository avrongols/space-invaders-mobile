using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject PauseMenuUI;

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        if (IsGamePaused)
    //        {
    //            Resume();
    //        }
    //        else
    //        {
    //            Pause();
    //        }
    //    }
    //}

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;

        AudioSource audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audio.Pause();
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;

        AudioSource audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audio.Play(0);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        PlayerPrefManager.ResetPlayerScore();
        IsGamePaused = false;
    }

    public void QuitGame()
    {
        IsGamePaused = false;
        Application.Quit();
    }
}
