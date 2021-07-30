using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5f;

    private Vector3 _pos;

    private void Update()
    {
        ChangePosition();
    }

    private void ChangePosition()
    {
        _pos = transform.position;
        _pos.y += -1 * MoveSpeed * Time.deltaTime;
        transform.position = _pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
