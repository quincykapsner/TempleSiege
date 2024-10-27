using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Statue : MonoBehaviour, IDamageable
{
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
    public float health = 100f;

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

    public void Defeated() { 
        // game over 
        chargeRate = 0; // stop charging
        string message = gameOverMessages[Random.Range(0, gameOverMessages.Count)];
        FindObjectOfType<GameController>().GameOver(message); 
    } 

    public void Win() {
        // in case i wanna do animation or something
        // camera focus on it and it turn white (and fade out?)
    }

    void FixedUpdate() {
        charge += chargeRate;
        if (charge >= 100) {
            // ==== success game over screen
            
        }
    }

}
