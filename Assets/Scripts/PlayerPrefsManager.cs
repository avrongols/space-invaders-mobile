using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class PlayerPrefManager
{
    public static int GetScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            return PlayerPrefs.GetInt("Score");
        }
        else
        {
            return 0;
        }
    }

    // story the current player state info into PlayerPrefs
    public static void SavePlayerScore(int score)
    {
        // save currentscore to PlayerPrefs for moving to next level
        PlayerPrefs.SetInt("Score", score);
    }

    // reset stored player state and variables back to defaults
    public static void ResetPlayerScore()
    {
        Debug.Log("Player State reset.");
        PlayerPrefs.SetInt("Score", 0);
    }

}
