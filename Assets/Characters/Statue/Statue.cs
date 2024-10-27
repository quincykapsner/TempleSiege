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

    public float charge = 0;

    public void OnHit(float damage, Vector2 knockback) { 
        Health -= damage;
        // no knockback for statue so just ignore it
    }  

    public void OnHit(float damage)
    {
        Health -= damage;
    }

    public void Defeated() { 
        // game over stuff
        // ==== camera focus on it and it turn white (and fade out?)
        // ==== call playercontroller.death ?
    } 

    void FixedUpdate() {
        charge += 1;
        if (charge >= 100) {
            // ==== success game over screen
        }
    }
}
