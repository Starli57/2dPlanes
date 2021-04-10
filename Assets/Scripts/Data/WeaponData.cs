using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponData : ScriptableObject
{
    [Space]
    public Sprite sprite;
    public Vector3 position;

    [Space]
    public float fireRate;
    public float reloadTime;
    public int bulletsCount;
}
