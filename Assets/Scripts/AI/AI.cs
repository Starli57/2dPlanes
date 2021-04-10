using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private float _attackAngle = 10;

    private PlaneController target = null;

    private PlaneController _plane;
    private WeaponController _weapon;

    private void Awake()
    {
        _plane = GetComponent<PlaneController>();
        _weapon = GetComponent<WeaponController>();
    }

    private void Update()
    {
        UpdateTarget();
        UpdateRotation();
        UpdateShooting();
    }

    private void UpdateTarget()
    {
        float minDistance = float.MaxValue;
        PlaneController bestTarget = null;

        var targets = TargetsManager.instance.GetTargetsWithoutThis(_plane);

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestTarget = target;
            }
        }

        target = bestTarget;
    }

    private void UpdateRotation()
    {
        if (target == null)
        {
            //todo: лететь куда-нибудь
            return;
        }

        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        _plane.UpdateRotation(targetRotation);
    }

    private void UpdateShooting()
    {
        float minAngle = 360;

        var targets = TargetsManager.instance.GetTargetsWithoutThis(_plane);

        foreach (var target in targets)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;            
            minAngle = Mathf.Min(minAngle, Vector3.Angle(transform.up, dir));
        }

        if (minAngle <= _attackAngle)
            _weapon.Shoot();
    }
}
