using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoveredDistanceCalculator : MonoBehaviour
{
    public float coveredDistance { get; private set; }

    private Vector3 _prevPosition;

    private void Awake()
    {
        _prevPosition = transform.position;
    }

    private void Update()
    {
        UpdateCoveredDistance(_prevPosition);

        _prevPosition = transform.position;
    }

    private void UpdateCoveredDistance(Vector3 prevPosition)
    {
        coveredDistance += Vector3.Distance(transform.position, prevPosition);
    }
}
