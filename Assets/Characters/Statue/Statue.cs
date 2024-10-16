using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour, IDamageable
{
    public float Health {
        set {
            health = value;
            if (health <= 0) {
                //what do when statue is destroyed
            }
        }
        get {
            return health;
        }
    }
    public float health = 100f;
    public void OnHit(float damage, Vector2 knockback) { }  // dont need, statue has no knockback

    public void OnHit(float damage)
    {
        Health -= damage;
    }

    public void RemoveEnemy()
    {
        throw new System.NotImplementedException();
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
