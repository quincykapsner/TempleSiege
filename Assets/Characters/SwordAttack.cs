using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{ 
    public float damage = 1f;
    public float knockbackForce = 500f;
    Vector2 attackOffset; 
    public Collider2D swordCollider;

    public void Start() {
        attackOffset = Vector2.zero;
        swordCollider.enabled = false; // make sure it starts disabled
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

    private void OnTriggerEnter2D(Collider2D collider) {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (damageableObject != null) {
            // calculate knockback
            Vector3 parentPosition = transform.parent.position;
            Vector2 direction = (collider.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            damageableObject.OnHit(damage, knockback);

        } else {
            Debug.Log("Collider doesnt implement IDamageable");
        }
    }
}
