using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject orc1;
    public GameObject orc2;
    public GameObject orc3; 

    public void SpawnRandomEnemy(int wave) {
        GameObject enemy = null;

        switch (wave) {
            case 1:
                enemy = orc1;
                break;
            case 2:
                enemy = Random.Range(0, 2) == 0 ? orc1 : orc2;
                break;
            case 3:
                int randomChoice = Random.Range(0, 3);
                enemy = randomChoice == 0 ? orc1 : randomChoice == 1 ? orc2 : orc3;  // Randomly orc1, orc2, or orc3 in wave 3
                break;
            default:
                break;
        }

        if (enemy != null) {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }

}
