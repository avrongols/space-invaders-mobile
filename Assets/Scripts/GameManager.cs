using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public int Score = 0;
    public int Health = 0;

    [SerializeField] private string LevelAfterVictory;
    [SerializeField] private string LevelAfterGameOver;
    [SerializeField] private Text UIScore;
    [SerializeField] private Text UIHealth;
    [SerializeField] private AudioClip WinningSFX;

    //[HideInInspector] public int _enemiesKilled;

    private GameObject _player;
    private Scene _scene;

    private void Awake()
    {
        // setup reference to game manager
        if (gm == null)
            gm = this.GetComponent<GameManager>();

        // setup all the variables, the UI, and provide errors if things not setup properly.
        SetupDefaults();
    }

    // setup all the variables, the UI, and provide errors if things not setup properly.
    private void SetupDefaults()
    {
        // get current scene
        _scene = SceneManager.GetActiveScene();

        // setup reference to player
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        if (_player == null)
            Debug.LogWarning("Player not found in Game Manager");

        // if levels not specified, default to current level
        if (LevelAfterVictory == "")
        {
            Debug.LogWarning("LevelAfterVictory not specified, defaulted to current level");
            LevelAfterVictory = _scene.name;
        }

        if (LevelAfterGameOver == "")
        {
            Debug.LogWarning("LevelAfterGameOver not specified, defaulted to current level");
            LevelAfterGameOver = _scene.name;
        }

        // friendly error messages
        if (UIScore == null)
            Debug.LogError("Need to set UIScore on Game Manager.");

        // get stored player prefs
        RefreshPlayerState();

        // get the UI ready for the game
        RefreshGUI();
    }

    // get stored Player Prefs if they exist, otherwise go with defaults set on gameObject
    private void RefreshPlayerState()
    {
        Score = PlayerPrefManager.GetScore();
        if (_player != null)
        {
            Health = _player.GetComponent<Spaceship>().Health;
        }
    }

    // refresh all the GUI elements
    private void RefreshGUI()
    {
        // set the text elements of the UI
        UIScore.text = "Score: " + Score.ToString();
        UIHealth.text = "Health: " + Health.ToString();
    }

    public void AddPoints(int amount)
    {
        Score += amount;
        UIScore.text = "Score: " + Score.ToString();
    }

    public void UpdateUIHealth(int health)
    {
        Health = health;
        UIHealth.text = "Health: " + Health.ToString();
    }

    // public function for level complete
    public void LevelCompete()
    {
        // save the current player prefs before moving to the next level
        PlayerPrefManager.SavePlayerScore(Score);

        // use a coroutine to allow the player to get fanfare before moving to next level
        StartCoroutine(LoadNextLevel());
    }

    // load the nextLevel after delay
    private IEnumerator LoadNextLevel()
    {
        // play winning sound effect
        AudioSource audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audio.clip = WinningSFX;
        audio.Play();

        yield return new WaitForSeconds(2.5f);

        if (LevelAfterVictory == "You Win")
        {
            // save the current player prefs before moving to the next level
            PlayerPrefManager.SavePlayerScore(Score);
        }
        SceneManager.LoadScene(LevelAfterVictory);
    }

    // public function to remove player life and reset game accordingly
    public void ResetGame()
    {
        // save the current player prefs before moving to the next level
        PlayerPrefManager.SavePlayerScore(Score);

        // load the gameOver screen
        SceneManager.LoadScene(LevelAfterGameOver);
    }
}
