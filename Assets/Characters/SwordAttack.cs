using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{ 
    public float damage = 1;
    Vector2 attackOffset; 
    Collider2D swordCollider;

    public void Start() {
        swordCollider = GetComponent<Collider2D>();
        attackOffset = transform.position;
    }

    public void AttackDown() {
        swordCollider.enabled = true;
        transform.position = attackOffset + new Vector2(0, -0.2f);
    }

    public void AttackUp() {
        swordCollider.enabled = true;
        transform.position = attackOffset + new Vector2(0, 0.2f);
    }

    public void AttackLeft() {
        swordCollider.enabled = true;
        transform.position = attackOffset + new Vector2(-0.2f, 0);
    }

    public void AttackRight() {
        swordCollider.enabled = true;
        transform.position = attackOffset + new Vector2(0.2f, 0);
    }

    public void StopAttack() {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            // deal damage to enemy
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.Health -= damage;
            }
        }
    }
}
