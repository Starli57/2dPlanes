using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlaneController))]
public class Player : MonoBehaviour
{
    private PlaneController _plane;
    private WeaponController _weapon;
    private FollowCamera _followCamera;

    private Vector2 _input;

    private void Awake()
    {
        _plane = GetComponent<PlaneController>();
        _weapon = GetComponent<WeaponController>();
        _followCamera = FindObjectOfType<FollowCamera>();

        _input = Vector2.up;
    }

    private void Update()
    {
        UpdateInput();
        UpdateRotation();
        UpdateShooting();
    }
    
    private void UpdateInput()
    {
        float inputX = 0;
        float inputY = 0;

        inputX = Mathf.Lerp(-1, 1, Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x));
        inputY = Mathf.Lerp(-1, 1, Mathf.InverseLerp(0, Screen.height, Input.mousePosition.y));

        _input = new Vector2(inputX, inputY);
    }

    private void UpdateRotation()
    {
        float angle = Mathf.Atan2(_input.y, _input.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        _plane.UpdateRotation(targetRotation);
    }

    private void UpdateShooting()
    {
        if (Input.GetMouseButton(0))
        {
            bool shooted = _weapon.Shoot();

            if (shooted)
                _followCamera.Shake();
        }
    }
}
