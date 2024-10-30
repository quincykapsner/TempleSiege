using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public EnemySpawner[] spawners;
    private int currentWave = 1;
    public float waveDuration = 30f;
    public float spawnInterval = 6f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves() { 
        while (currentWave <= 3) {
            StartCoroutine(SpawnEnemies(currentWave));
            yield return new WaitForSeconds(waveDuration);
            currentWave++;
        }
        Debug.Log("all waves completed"); 
    }

    IEnumerator SpawnEnemies(int wave) {
        float elapsedTime = 0f;
        while (elapsedTime < waveDuration) {
            foreach (var spawner in spawners) {
                spawner.SpawnRandomEnemy(wave);
            }
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
        }
    }
}
