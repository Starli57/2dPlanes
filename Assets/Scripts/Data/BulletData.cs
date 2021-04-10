using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BulletData : ScriptableObject
{
    [Space]
    public GameObject bulletPrefab;
    public GameObject destroyEffect;

    [Space]
    public float damage;
    public float bulletSpeed;
    public float maxDistance;

    [Space]
    public bool autoGuidance = false;
    public float guidanceManevr = 5;
}
