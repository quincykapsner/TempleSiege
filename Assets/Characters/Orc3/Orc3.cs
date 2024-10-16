using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Orc3 : Enemy
{
    //public List<Collider2D> detectedObjs = new List<Collider2D>();
    public GameObject statue;
    public GameObject player;
    public Collider2D detectionCollider; // collider for detecting player
    public Collider2D hitboxCollider; // collider for hitbox

    // Start is called before the first frame update
    public override void Start()
    { 
        base.Start();
        health = 3f;
        statue = GameObject.FindGameObjectWithTag("Statue");
        detectionCollider = GetComponent<CircleCollider2D>();
        hitboxCollider = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate() {
        if(!atSomething){
            if (player != null) {
                // if player is detected, move towards player
                Vector2 direction = (player.transform.position - transform.position).normalized; // calculate direction towards player
                rb.AddForce(direction * moveSpeed * Time.deltaTime); // move towards player

            } else if (statue != null) {
                // if player isnt detected and statue exists, move towards statue
                Vector2 direction = (statue.transform.position - transform.position).normalized; // calculate direction towards statue
                rb.AddForce(direction * moveSpeed * Time.deltaTime); // move towards statue
            }
        }

    }

    void OnTriggerEnter2D(Collider2D detectedObj) {
        // this triggers when player enters detection range
        if (detectedObj.CompareTag("Player")) {
            atSomething = false; // allow the orc3 to move again if it was at the statue
            player = detectedObj.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D detectedObj) {
        // this triggers when player exits detection range
        if (detectedObj.CompareTag("Player")) {
            player = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // this triggers when enemy hitbox collides with player or statue
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Statue")) {
            atSomething = true;
            rb.velocity = Vector2.zero; // stop moving

            // ====== add attacking stuff
        } 
    }

    void OnCollisionExit2D(Collision2D collision) {
        // this triggers when enemy hitbox no longer collides with player or statue
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Statue")) { 
            atSomething = false;
        }
    }
}
