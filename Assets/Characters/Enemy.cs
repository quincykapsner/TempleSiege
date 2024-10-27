using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    Animator animator;
    public Rigidbody2D rb;

    public float moveSpeed = 50f;
    //public bool atSomething = false; // tracking when enemy is next to something
    public bool canMove = true;
    public float health;

    public float Health {
        set {
            health = value;
            if (health <= 0) {
                Death();
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

    public void Death() {
        // must be separate from defeated to allow for animations
        animator.SetTrigger("Defeated");
    }

    public void Defeated() {
        // called in animation event
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

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
}
