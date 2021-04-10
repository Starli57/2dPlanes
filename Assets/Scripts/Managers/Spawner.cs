using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static PlaneController globalPlayer { get; private set; }

    [SerializeField] private PlaneController _planePrefab;
    [SerializeField] private List<PlaneData> _planeData;

    [Space]
    [SerializeField] private Vector2 _xLimits;
    [SerializeField] private Vector2 _yLimits;

    [Space]
    [SerializeField] private float _playersSpawnTime = 2;
    [SerializeField] private float _botsSpawnTime = 2;
    
    private float playerPlaneDestroyedTime = float.MinValue;
    private float botLastSpawn = float.MinValue;

    private void Update()
    {
        if (Time.time - botLastSpawn > _botsSpawnTime)
            SpawnBot();

        if (Time.time - playerPlaneDestroyedTime > _playersSpawnTime && globalPlayer == null)
            SpawnPlayer();
    }

    private void SpawnBot()
    {
        var plane = SpawnPlane();
        plane.gameObject.AddComponent<AI>();
        botLastSpawn = Time.time;
    }

    private void SpawnPlayer()
    {
        globalPlayer = SpawnPlane();
        globalPlayer.gameObject.AddComponent<Player>();
        globalPlayer.onHealthOver += OnPlayerPlaneDestroyed;
    }
    
    private PlaneController SpawnPlane()
    {
        Vector3 position = new Vector3(Random.Range(_xLimits.x, _xLimits.y), Random.Range(_yLimits.x, _yLimits.y), 0);
        PlaneData data = _planeData[Random.Range(0, _planeData.Count)];

        PlaneController plane = Instantiate(_planePrefab, position, Quaternion.identity);
        plane.SetData(data);

        return plane;
    }

    private void OnPlayerPlaneDestroyed()
    {
        playerPlaneDestroyedTime = Time.time;

        globalPlayer.onHealthOver -= OnPlayerPlaneDestroyed;
        globalPlayer = null;
    }
}
