using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitDetector))]
public class Health : MonoBehaviour
{
    public System.Action onHealthOver;

    public void Hit(Transform hitter, Vector3 direction)
    {
        int hitterLayer = hitter.gameObject.layer;

        if (hitterLayer == 4)//water
        {
            ChangeHealth(-_maxHealth);
        }
        else if (hitterLayer == 8)//plane
        {
            ChangeHealth(-_maxHealth);
        }
        else if (hitterLayer == 9)//bullet
        {
            Bullet bullet = hitter.gameObject.GetComponent<Bullet>();
            if (bullet != null)
                ChangeHealth(-bullet.bulletData.damage);
        }        
    }
    
    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;

        RestoreHealth();
    }


    private HitDetector _hitDetector;

    private float _maxHealth = 100;
    private float _health;

    private void Awake()
    {
        _hitDetector = GetComponent<HitDetector>();

        RestoreHealth();
    }

    private void OnEnable()
    {
        _hitDetector.onHitted += Hit;
    }

    private void OnDisable()
    {
        _hitDetector.onHitted -= Hit;
    }

    private void ChangeHealth(float diff)
    {
        _health += diff;

        if (_health <= 0)
            onHealthOver?.Invoke();
    }

    private void RestoreHealth()
    {
        _health = _maxHealth;
    }
}
