using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int KillPoints = 1;
    [SerializeField] private int Health = 1;
    [SerializeField] private GameObject EnemyLaser;
    [SerializeField] public AudioClip EnemyLaserSFX;
    [SerializeField] public AudioClip DamageSFX;
    
    [Tooltip("The number of seconds in which approximately one random shot occurs")] 
    [SerializeField] private float SecondsForShot = 10.0f;

    private float _bottomLine = -4.2f;
    private float _deathAnimationLength = 0.3f;
    private float _laserOffsetY = -0.5f;
    private AudioSource _audio;
    private Animator _animator;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        { // if AudioSource is missing
            Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
            // let's just add the AudioSource component dynamically
            _audio = gameObject.AddComponent<AudioSource>();
        }

        _animator = GetComponent<Animator>();
        if (_animator == null) // if Animator is missing
            Debug.LogError("Animator component missing from this gameobject");
    }

    private void Update()
    {
        CheckBottomLine();
        RandomFire();
    }

    private void CheckBottomLine()
    {
        Vector3 pos = transform.position;
        if (pos.y < _bottomLine)
        {
            if (GameManager.gm) // if the gameManager is available, tell it to reset the game
                GameManager.gm.ResetGame();
            else // otherwise, just reload the current level
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // kill the enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (Health > 1)
        {
            Health--;
        }
        else
        {
            StartCoroutine(KillEnemy());
        }
        // play sound
        PlaySound(DamageSFX);
    }

    // coroutine to kill the enemy
    private IEnumerator KillEnemy()
    {
        // play the death animation
        _animator.SetTrigger("hit");

        // after waiting tell the GameManager to reset the game
        yield return new WaitForSeconds(_deathAnimationLength);

        // increase points
        if (GameManager.gm)
        {
            GameManager.gm.AddPoints(KillPoints);
            //GameManager.gm.AddEnemiesKilled();

            // check if it is the last enemy
            CheckLastEnemy();
        }
        Destroy(this.gameObject);
    }

    private void CheckLastEnemy()
    {
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (_enemies.Length <= 1)
        {
            GameManager.gm.LevelCompete();
        }
    }

    private void RandomFire()
    {
        if (Random.Range(0.0f, 1.0f) <= 1.0f / (60.0f * SecondsForShot)
            && PauseMenuController.IsGamePaused == false)
        {
            Vector3 laserPos = transform.position;
            laserPos.y += _laserOffsetY;
            Instantiate(EnemyLaser, laserPos, transform.rotation);
            PlaySound(EnemyLaserSFX);
        }
    }

    private void PlaySound(AudioClip sound)
    {
        _audio.PlayOneShot(sound);
    }
}
