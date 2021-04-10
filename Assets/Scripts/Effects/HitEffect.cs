using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private void Awake()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 180));
    }
}
