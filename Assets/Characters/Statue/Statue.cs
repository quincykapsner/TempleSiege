using System.Collections;
using System.Collections.Generic;
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
    } 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
