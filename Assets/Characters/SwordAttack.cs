using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{ 
    public float damage = 1;
    Vector2 attackOffset; 
    public Collider2D swordCollider;

    public void Start() {
        attackOffset = Vector2.zero;
    }

    public void AttackDown() {
        swordCollider.enabled = true;
        transform.localPosition = attackOffset + new Vector2(0, -0.15f);
    }

    public void AttackUp() {
        swordCollider.enabled = true;
        transform.localPosition = attackOffset + new Vector2(0, 0.15f);
    }

    public void AttackLeft() {
        swordCollider.enabled = true;
        transform.localPosition = attackOffset + new Vector2(-0.15f, 0);
    }

    public void AttackRight() {
        swordCollider.enabled = true;
        transform.localPosition = attackOffset + new Vector2(0.15f, 0);
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
