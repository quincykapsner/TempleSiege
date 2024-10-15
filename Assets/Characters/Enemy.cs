using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;

    public float health;

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

    public virtual void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Defeated() {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }
}
