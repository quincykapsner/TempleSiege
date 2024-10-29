using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject orc1;
    public GameObject orc2;
    public GameObject orc3;
    public float spawnRate = 1f;

    public void SpawnWave(int wave, System.Action onWaveComplete)
    {
        StartCoroutine(SpawnEnemies(wave, onWaveComplete));
    }

    IEnumerator SpawnEnemies(int wave, System.Action onWaveComplete)
    {
        switch (wave) {
            case 1:
                Instantiate(orc1, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(orc1, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnRate);
                Instantiate(orc1, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(orc2, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnRate);
                Instantiate(orc1, transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(orc2, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnRate);
                Instantiate(orc1, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnRate);
                Instantiate(orc1, transform.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(orc1, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnRate);
                Instantiate(orc2, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnRate);
                Instantiate(orc3, transform.position, Quaternion.identity);
                break;
        }
        yield return new WaitForSeconds(1f);
        onWaveComplete.Invoke();
    }

}
