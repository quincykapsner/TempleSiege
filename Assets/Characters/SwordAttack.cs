using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{ 
    public float damage = 1f;
    public float knockbackForce = 500f;
    public float reach = 0.15f;
    public Collider2D swordCollider;

    public void Start() {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false; // make sure it starts disabled
    }

    public void AttackDown() {
        transform.localPosition = new Vector2(0, -reach);
        swordCollider.enabled = true;
    }

    public void AttackUp() {
        transform.localPosition = new Vector2(0, reach);
        swordCollider.enabled = true;
    }

    public void AttackLeft() {
        transform.localPosition = new Vector2(-reach, 0);
        swordCollider.enabled = true;
    }

    public void AttackRight() {
        transform.localPosition = new Vector2(reach, 0);
        swordCollider.enabled = true;
    }

    public void StopAttack() {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (damageableObject != null) {
            // ====== add code to not do any damage to the statue
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
