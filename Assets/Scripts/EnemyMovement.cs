using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float DescentLength = 0.5f;
    [SerializeField] private float MoveSpeed = 2.0f;
    [SerializeField] private float IndentFromBorders = 0.75f;

    private Vector3 _pos;
    private int _dir = -1;

    private void Start()
    {
        _pos = transform.position;
    }

    private void Update()
    {
        ChangePosition();
        CheckBorders();
    }

    private void ChangePosition()
    {
        _pos.x += MoveSpeed * _dir * Time.deltaTime;
        transform.position = _pos;
    }

    private void CheckBorders()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraHalfHeight = Camera.main.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * screenAspect;
        //float height = cameraHeight;

        if (transform.position.x <= -cameraHalfWidth + IndentFromBorders
            && PauseMenuController.IsGamePaused == false)
        {
            _dir = 1;
            _pos.y -= DescentLength;
            transform.position = new Vector2(-cameraHalfWidth + IndentFromBorders, _pos.y);
        }

        if (transform.position.x >= cameraHalfWidth - IndentFromBorders
            && PauseMenuController.IsGamePaused == false)
        {
            _dir = -1;
            _pos.y -= DescentLength;
            transform.position = new Vector2(cameraHalfWidth - IndentFromBorders, _pos.y);
        }
    }
}
