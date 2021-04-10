using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public Action onHealthOver;

    public PlaneData data { get; private set; }

    public void SetData(PlaneData data)
    {
        this.data = data;
        
        _weaponController.SetWeaponData(data.weapon);
        _weaponController.SetBulletData(data.bullet);
        
        _health.SetMaxHealth(data.maxHealth);
    }

    public void UpdateRotation(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * data.maneuverability);
    }
        
    private WeaponController _weaponController;
    private Health _health;

    private Vector2 _velocity;

    private void Awake()
    {
        _weaponController = GetComponent<WeaponController>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        TargetsManager.instance.Add(this);

        _health.onHealthOver += OnHealthOver;
    }

    private void OnDisable()
    {
        if (TargetsManager.instance != null)
            TargetsManager.instance.Remove(this);

        _health.onHealthOver -= OnHealthOver;
    }

    private void FixedUpdate()
    {
        AddFriction(ref _velocity);
        AddAcceleration(ref _velocity);
        AddGravity(ref _velocity);
        ClampByLimit(ref _velocity, data.velocityLimit);

        transform.position += new Vector3(_velocity.x, _velocity.y, 0);
    }

    private void AddFriction(ref Vector2 velocity)
    {
        velocity -= velocity.normalized * data.friction;
    }

    private void AddAcceleration(ref Vector2 velocity)
    {
        velocity += new Vector2(transform.up.x, transform.up.y) * data.acceleration;
    }

    private void AddGravity(ref Vector2 velocity)
    {
        velocity -= Vector2.up * data.gravity * transform.position.y * data.heightOxigenPenalty;
    }

    private void ClampByLimit(ref Vector2 velocity, float limit)
    {
        velocity = new Vector2(Mathf.Clamp(velocity.x, -limit, limit), Mathf.Clamp(velocity.y, -limit, limit));
    }

    private void OnHealthOver()
    {
        onHealthOver?.Invoke();

        EffectsManager.instance.ShowEffect(EffectType.planeDestroy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
