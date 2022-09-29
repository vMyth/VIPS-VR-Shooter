using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float destroyTime = 5f;

    private void Awake()
    {
        Destroy(gameObject, destroyTime);   
    }
}
