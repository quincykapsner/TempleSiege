using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Statue : MonoBehaviour, IDamageable
{
    private bool isDefeated = false;
    public float Health {
        set {
            health = value;
            if (health <= 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }
    public float health; 
    public float maxHealth = 100f;

    public float charge = 0f;
    public float chargeRate = 0.01f;

    // List of game over messages
    private List<string> gameOverMessages = new List<string>
    {
        "The statue has been destroyed!",
        "The power of darkness has prevailed...",
        "The goddess is no more...", 
        "Eyy I'm gettin' destroyed by orcs ovah here!"
    };

    public void OnHit(float damage, Vector2 knockback) { 
        Health -= damage;
        // no knockback for statue so just ignore it
    }  

    public void OnHit(float damage) {
        Health -= damage;
    }

    public int getScore() {
        return (int)charge;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    void FixedUpdate() {
        charge += chargeRate;
        if (charge >= 100) {
            Win();
        }
    }
    
    public void Defeated() { 
        if (isDefeated) return; // only do this once

        isDefeated = true;
        chargeRate = 0; // stop charging
        // game over 
        string message = gameOverMessages[Random.Range(0, gameOverMessages.Count)];
        FindObjectOfType<GameController>().GameOver(message); 
    } 

    public void Win() {
        chargeRate = 0; // stop charging
        // kill all enemies
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies) {
            enemy.Death();
        }
        // play some victory music
        // fade to whitish cyan?
        // maybe if i can figure out camera stuff do that - maybe secondary camera?
        FindObjectOfType<GameController>().Win();
    }
}
