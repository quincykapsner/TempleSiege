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
                // wave 1 can only spawn orc1
                enemy = orc1;
                break;
            case 2:
                // wave 2: orc1 or orc2
                enemy = Random.Range(0, 2) == 0 ? orc1 : orc2;
                break;
            case 3:
                // wave 3: orc1, orc2, or orc3 
                int randomChoice = Random.Range(0, 3);
                enemy = randomChoice == 0 ? orc1 : randomChoice == 1 ? orc2 : orc3; 
                break;
            default:
                break;
        }

        if (enemy != null) {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }

}
