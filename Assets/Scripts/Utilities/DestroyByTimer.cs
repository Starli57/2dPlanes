using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTimer : MonoBehaviour
{
    [SerializeField] private float _destroyTime = 0.2f;

    private float _spawnTime = 0;

    private void Awake()
    {
        _spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _spawnTime > _destroyTime)
            Destroy(gameObject);
    }
}
