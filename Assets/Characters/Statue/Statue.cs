using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Statue : MonoBehaviour, IDamageable
{
    bool defeated = false;
    bool critical = false;
    public float Health {
        set {
            health = value;
            if (health <= maxHealth / 5 && !critical) {
                critical = true;
                CriticalHealth();
            } else if (health <= 0 && !defeated) {
                defeated = true;
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

    AudioManager audioManager;

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
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        health = maxHealth;
    }

    void FixedUpdate() {
        charge += chargeRate;
        if (charge >= 100) {
            Win();
        }
    }

    public void CriticalHealth() {
        audioManager.PlaySFX(audioManager.criticalHealth);
    }
    
    public void Defeated() { 
        chargeRate = 0; // stop charging
        audioManager.PlaySFX(audioManager.gameOver);
        // game over 
        string message = gameOverMessages[Random.Range(0, gameOverMessages.Count)];
        FindObjectOfType<GameController>().GameOver(message); 
    } 

    public void Win() {
        chargeRate = 0; // stop charging
        audioManager.PlaySFX(audioManager.win);
        // kill all enemies
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies) {
            enemy.Death();
        }
        FindObjectOfType<GameController>().Win();
    }
}
