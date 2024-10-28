using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    private float startSpawnTime;
    private float endSpawnTime;
    private float spawnInterval = 1.0f;

    void FixedUpdate()
    {
        
    }
}
