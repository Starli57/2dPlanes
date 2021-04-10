
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool Shoot()
    {
        if (Time.time - _lastShootTime < _weaponData.fireRate)
            return false;

        if (_reloadingTimer > 0 || _bulletsCount <= 0)
            return false;

        float angle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        Bullet bullet = Instantiate(_bulletData.bulletPrefab, transform.position, targetRotation).GetComponent<Bullet>();
        bullet.SetBulletData(_bulletData);
        bullet.SetOwner(gameObject);
        bullet.SetTarget(GetBestTarget());

        _lastShootTime = Time.time;
        _bulletsCount--;

        if (_bulletsCount <= 0)
            StartCoroutine(ReloadBulletsByTimer());

        return true;
    }

    public void SetWeaponData(WeaponData weaponData)
    {
        _weaponData = weaponData;

        ReloadBullets();
    }

    public void SetBulletData(BulletData bulletData)
    {
        _bulletData = bulletData;
    }

    private WeaponData _weaponData;
    private BulletData _bulletData;

    private float _lastShootTime = float.MinValue;
    private float _reloadingTimer = 0;

    private int _bulletsCount;

    private void Update()
    {
        _reloadingTimer -= Time.deltaTime;
    }

    private IEnumerator ReloadBulletsByTimer()
    {
        yield return new WaitForSeconds(_weaponData.reloadTime);
        ReloadBullets();
    }

    private void ReloadBullets()
    {
        _bulletsCount = _weaponData.bulletsCount;
    }

    private Transform GetBestTarget()
    {
        var possible = TargetsManager.instance.GetTargetsWithoutThis(GetComponent<PlaneController>());

        PlaneController bestByAngle = GetBestTargetByAngle(possible);
        if (bestByAngle != null)
            return bestByAngle.transform;

        PlaneController closest = GetClosestTarget(possible);
        return closest != null ? closest.transform : null; ;
    }

    private PlaneController GetBestTargetByAngle(List<PlaneController> possible)
    {
        float bestAngle = 360;
        PlaneController bestByAngle = null;

        foreach (var target in possible)
        {
            float angle = Vector3.Angle(transform.up, (target.transform.position - transform.position).normalized);
            if (angle < bestAngle)
            {
                bestAngle = angle;
                bestByAngle = target;
            }
        }

        return bestByAngle;
    }

    private PlaneController GetClosestTarget(List<PlaneController> possible)
    {
        float minDistance = float.MaxValue;
        PlaneController closest = null;

        foreach (var target in possible)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = target;
            }
        }
        
        return closest;
    }
}
