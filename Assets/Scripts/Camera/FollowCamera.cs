using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public void Shake()
    {
        _shakeTime = 0;
    }

    [SerializeField] private Vector3 _offset;

    [SerializeField] private AnimationCurve _shakeCurve;
    [SerializeField] private float _shakePower = 1;

    private float _shakeTime = float.MaxValue;
    private Vector3 _shakeOffset;


    private void Update()
    {
        _shakeOffset = Random.insideUnitCircle * _shakeCurve.Evaluate(_shakeTime) * _shakePower;
        _shakeTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (Spawner.globalPlayer != null)
            transform.position = Spawner.globalPlayer.transform.position + _offset + _shakeOffset;
        
    }
}
