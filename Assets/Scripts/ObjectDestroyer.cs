using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] private float TimeOut = 3.0f;

    private void Awake()
    {
        Invoke("DestroyObject", TimeOut);
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);        
    }
}
