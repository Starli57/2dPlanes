using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlaneData : ScriptableObject
{
    [Header("Data")]
    public WeaponData weapon;
    public BulletData bullet;

    [Header("Health")]
    public int maxHealth;

    [Header("Movement")]
    public float maneuverability = 3;
    public float velocityLimit = 100;
    public float acceleration = 1;
    public float gravity = 1;
    public float friction = 1f;
    public float heightOxigenPenalty = 0.1f;//позволяет ограничить максимальную высоту полетов
}
