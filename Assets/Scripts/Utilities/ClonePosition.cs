using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePosition : MonoBehaviour
{
    [SerializeField] private bool _cloneX = false;
    [SerializeField] private bool _cloneY = false;
    [SerializeField] private bool _cloneZ = false;

    private void LateUpdate()
    {
        if (Spawner.globalPlayer == null)
            return;

        Transform player = Spawner.globalPlayer.transform;
        float x = _cloneX ? player.position.x : transform.position.x;
        float y = _cloneY ? player.position.y : transform.position.y;
        float z = _cloneZ ? player.position.z : transform.position.z;

        transform.position = new Vector3(x, y, z);
    }
}
