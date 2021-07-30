using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class SpaceshipMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float IndentFromBorders = 1.0f;

    //private Vector3 _pos;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ChangePosition();
        CheckBorders();
    }

    private void ChangePosition()
    {
        float dirX = CrossPlatformInputManager.GetAxis("Horizontal") * MoveSpeed * 10;
        _rb.velocity = new Vector2(dirX, 0);

        if (Input.GetKey("left"))
        {
            dirX = (-1 * MoveSpeed);
            _rb.velocity = new Vector2(dirX, 0);
        }
        else if (Input.GetKey("right"))
        {
            dirX = (1 * MoveSpeed);
            _rb.velocity = new Vector2(dirX, 0);
        }
    }

    private void CheckBorders()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHalfHeight = Camera.main.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * screenAspect;
        //float height = cameraHeight;

        if (transform.position.x <= -cameraHalfWidth + IndentFromBorders)
        {
            transform.position = new Vector2(-cameraHalfWidth + IndentFromBorders, transform.position.y);
        }

        if (transform.position.x >= cameraHalfWidth - IndentFromBorders)
        {
            transform.position = new Vector2(cameraHalfWidth - IndentFromBorders, transform.position.y);
        }
    }
}







//    void Update()
//    {
//        // move the player left or right
//        if (Input.GetKey("left"))
//        {
//            dir = -1;
//        }
//        else if (Input.GetKey("right"))
//        {
//            dir = 1;
//        }
//        else
//        {
//            dir = 0;
//        }
//
//        pos = transform.position;
//        pos.x += dir * speed * Time.deltaTime;
//        transform.position = pos;
//
//        float cameraHeight = Camera.main.orthographicSize;
//        float screenAspect = (float)Screen.width / Screen.height;
//        float width = cameraHeight * screenAspect;
//        //float height = cameraHeight;
//
//        if (transform.position.x <= -width + 1f)
//        {
//            transform.position = new Vector2(-width + 1f, transform.position.y);
//        }
//
//        if (transform.position.x >= width - 1f)
//        {
//            transform.position = new Vector2(width - 1f, transform.position.y);
//        }
//
//        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
//        {
//            // instantiate laser on top of spaceship position
//            Vector3 laserPos = transform.position;
//            laserPos.y = -5.2f;
//            newlaser = Instantiate(laser, laserPos, transform.rotation) as GameObject;
//
//            // play sound
//            playSound(laserSFX);
//        }
//    }
