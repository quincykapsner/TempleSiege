using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public EnemySpawner[] spawners;
    public int currentWave = 1;
    private bool waveInProgress = false;
    public float waveDelay = 30f; // wait time between waves

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        while (currentWave <= 5) // hardcoded for 5 waves
        {
            waveInProgress = true;
            SpawnWave(currentWave);
            yield return new WaitUntil(() => !waveInProgress);
            yield return new WaitForSeconds(waveDelay); // wait time between waves
            currentWave++;
        }
        Debug.Log("all waves completed"); // all waves complete
    }

    void SpawnWave(int wave)
    {
        foreach (var spawner in spawners)
        {
            spawner.SpawnWave(wave, () => waveInProgress = false);
        }
    }
}
