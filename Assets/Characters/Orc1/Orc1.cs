using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc1 : Enemy
{
    public GameObject statue;
    public Collider2D hitboxCollider; // collider for hitbox

    // Start is called before the first frame update
    public override void Start()
    { 
        base.Start();
        health = 1f;
        statue = GameObject.FindGameObjectWithTag("Statue");
        hitboxCollider = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate() {
        if (!atSomething && statue != null) {
            // if statue exists, move towards statue
            Vector2 direction = (statue.transform.position - transform.position).normalized; // calculate direction towards statue
            rb.AddForce(direction * moveSpeed * Time.deltaTime); // move towards statue
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // this triggers when enemy hitbox collides with statue
        if (collision.collider.CompareTag("Statue")) { 
            atSomething = true;
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        // this triggers when enemy hitbox no longer collides with statue
        if (collision.collider.CompareTag("Statue")) { 
            atSomething = false;
        }
    }

}
