using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PelletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _pelletPrefab;

    [Range(1,20)]
    [field: SerializeField] public int NumberToSpawn;
    
    [SerializeField] private Vector2 _arenaSize;
    private Vector2 _arenaExtents;
    
    private Vector2[] _pelletPositions;

    private float _detectionRadius = 1f;
    
    private void Start()
    {
        _arenaExtents = _arenaSize * 0.5f;
        
    }

    public void SpawnPellets()
    {
        _pelletPositions = new Vector2[NumberToSpawn];

        for (int i = 0; i < NumberToSpawn; i++)
        {
            while (_pelletPositions[i] == Vector2.zero)
            {
                float xPos = Random.Range(-_arenaExtents.x, _arenaExtents.x);
                float zPos = Random.Range(-_arenaExtents.y, _arenaExtents.y);
                
                Vector2 pelletPosition = new Vector2(xPos, zPos);

                if (NearAnotherPellet(pelletPosition)) continue;
                
                _pelletPositions[i] = pelletPosition;
                
                SpawnPellet(pelletPosition);
            }
        }
    }

    private void SpawnPellet(Vector2 position)
    {
        Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
        Debug.Log("Pellet spawn ediliyor: " + worldPosition);
        GameObject pellet = Instantiate(_pelletPrefab, worldPosition, Quaternion.identity);
    }
    
    private bool NearAnotherPellet(Vector2 pelletPosition)
    {
        for (int i = 0; i < _pelletPositions.Length; i++)
        {
            if ((pelletPosition - _pelletPositions[i]).magnitude < _detectionRadius) return true;
        }
        
        return false;
    }
}
