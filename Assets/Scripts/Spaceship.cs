using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Spaceship : MonoBehaviour
{
    [SerializeField] public int Health = 3;
    [SerializeField] private int MaxNumberOfLasers = 4;
    [SerializeField] private GameObject Laser;
    [SerializeField] public AudioClip LaserSFX;
    [SerializeField] public AudioClip DamageSFX;

    private AudioSource _audio;
    private GameObject[] _lasers;
    private float _laserOffsetY = 0.75f;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        { // if AudioSource is missing
            Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
            // let's just add the AudioSource component dynamically
            _audio = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        CheckFire();
    }

    private void CheckFire()
    {
        _lasers = GameObject.FindGameObjectsWithTag("Laser");
        if ((CrossPlatformInputManager.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
            && _lasers.Length < MaxNumberOfLasers
            && PauseMenuController.IsGamePaused == false)
        {
            Vector3 laserPos = transform.position;
            laserPos.y = transform.position.y + _laserOffsetY;
            Instantiate(Laser, laserPos, transform.rotation);

            PlaySound(LaserSFX);
        }
    }

    private void TakeDamage()
    {
        if (Health > 1)
        {
            Health--;
            GameManager.gm.UpdateUIHealth(Health);
        }
        else
        {
            GameManager.gm.ResetGame();
        }
        PlaySound(DamageSFX);
    }

    private void PlaySound(AudioClip sound)
    {
        _audio.PlayOneShot(sound);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy Laser")
        {
            TakeDamage();
        }
    }
}