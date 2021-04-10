using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData { get; private set; }

    public void SetBulletData(BulletData bulletData)
    {
        this.bulletData = bulletData;
    }

    public void SetOwner(GameObject owner)
    {
        _owner = owner;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    private CoveredDistanceCalculator _distanceCalculator;

    private Vector3 _prevPosition;

    private GameObject _owner;
    private Transform _target;
    
    private void Awake()
    {
        _distanceCalculator = GetComponent<CoveredDistanceCalculator>();

        _prevPosition = transform.position;
    }

    private void Update()
    {
        UpdateGuidance();
        Move();

        if (IsInDistanceLimit())
            CheckHit();
        else
            Explode(transform.position);
        
        _prevPosition = transform.position;
    }

    private void UpdateGuidance()
    {
        if (bulletData.autoGuidance && _target != null)
        {
            Vector3 dir = (_target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * bulletData.guidanceManevr);
        }
    }

    private void Move()
    {
        Vector2 velocity = transform.up * bulletData.bulletSpeed * Time.deltaTime;
        transform.position += new Vector3(velocity.x, velocity.y, 0);
    }

    private void CheckHit()
    {
        var hits = Physics2D.RaycastAll(transform.position, (_prevPosition - transform.position).normalized, Vector3.Distance(transform.position, _prevPosition));
        if (hits.Length > 0)
        {
            OnHit(hits);
        }
    }

    private void OnHit(RaycastHit2D[] hits)
    {
        bool hitted = false;

        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject == _owner)
                continue;

            hitted = true;

            Health health = hits[i].transform.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.Hit(transform, (transform.position - _prevPosition).normalized);
            }
        }

        if (hitted)
        {
            transform.position = hits[0].point;
            Explode(transform.position);
        }
    }

    private void Explode(Vector3 position)
    {
        Instantiate(bulletData.destroyEffect, position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    private bool IsInDistanceLimit()
    {
        return _distanceCalculator.coveredDistance <= bulletData.maxDistance;
    }
}
