using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Orc3 : Enemy
{
    //public List<Collider2D> detectedObjs = new List<Collider2D>();
    public GameObject statue;
    public Collider2D hitboxCollider; // collider for hitbox
    public GameObject player;
    public Collider2D detectionCollider; // collider for detecting player

    public SwordAttack swordAttack;
    private Coroutine attackCoroutine; // coroutine for attacking statue

    private Animator animator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    AudioManager audioManager;

    // Start is called before the first frame update
    public override void Start()
    { 
        base.Start();
        health = 3f;
        statue = GameObject.FindGameObjectWithTag("Statue");
        hitboxCollider = GetComponent<CapsuleCollider2D>();
        detectionCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void FixedUpdate() {
        // if allowed to move
        if(canMove){
            Vector2 direction = Vector2.zero;
            Vector2 target = Vector2.zero;

            if (player != null) {
                // if player is detected, move towards player
                target = statue.GetComponent<Collider2D>().ClosestPoint(transform.position); // get closest point on statue collider
                direction = (target - (Vector2)transform.position).normalized; // calculate direction towards statue
                rb.AddForce(direction * moveSpeed * Time.deltaTime); // move towards player

            } else if (statue != null) {
                // if player isnt detected and statue exists, move towards statue
                direction = (statue.transform.position - transform.position).normalized; // calculate direction towards statue
                rb.AddForce(direction * moveSpeed * Time.deltaTime); // move towards statue
            }

            // walk animation
            if (direction != Vector2.zero) {
                // if enemy is moving, save directions and set animator values
                animator.SetFloat(lastHorizontal, direction.x);
                animator.SetFloat(lastVertical, direction.y);

                animator.SetFloat(horizontal, direction.x);
                animator.SetFloat(vertical, direction.y);
            } else {
                // if enemy is not moving, set animator values to 0
                animator.SetFloat(horizontal, 0);
                animator.SetFloat(vertical, 0);
            }
        }

    }

    // ========= player detect stuff ========= 
    /* currently circle collider is turned off bc player attacks hit it but i 
    need to focus on other stuff 
    void OnTriggerEnter2D(Collider2D detectedObj) {
        // this triggers when player enters detection range
        if (detectedObj.CompareTag("Player")) {
            UnlockMovement(); // allow the orc3 to move again if it was at the statue
            player = detectedObj.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D detectedObj) {
        // this triggers when player exits detection range
        if (detectedObj.CompareTag("Player")) {
            player = null;
        }
    }*/

    // ========= hitbox stuff ========= 
    void OnCollisionEnter2D(Collision2D collision) {
        // this triggers when enemy hitbox collides with player or statue
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Statue")) {
            // stop moving
            LockMovement();
            rb.velocity = Vector2.zero; 

            // stop movement animation
            animator.SetFloat(horizontal, 0);
            animator.SetFloat(vertical, 0);

            // attack statue
            if (attackCoroutine == null) {
                attackCoroutine = StartCoroutine(AttackStatue());
            }
        } 
    }

    void OnCollisionExit2D(Collision2D collision) {
        // this triggers when enemy hitbox no longer collides with player or statue
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Statue")) { 
            UnlockMovement();
            if (attackCoroutine != null) {
                StopCoroutine(attackCoroutine); // stop attacking when leaving statue or player leaves
                attackCoroutine = null;
            }
        }
    }

    private IEnumerator AttackStatue() { 
        while (true) {
            PerformAttack();
            yield return new WaitForSeconds(1f);
        }
    }

    private void PerformAttack() {
        // attack animation
        audioManager.PlaySFX(audioManager.orcAttack);
        animator.SetTrigger("SwordAttack");
    }

    public void SwordAttack() {
        // called in animation event at start of attack animation
        LockMovement();

        // determine attack direction based on direction to statue
        Vector2 target = statue.GetComponent<Collider2D>().ClosestPoint(transform.position); // get closest point on statue collider
        Vector2 attackDirection = (target - (Vector2)transform.position).normalized;
        if (Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y)) {
            // prioritize horizontal direction
            if (attackDirection.x > 0) {
                swordAttack.AttackRight();
            } else {
                swordAttack.AttackLeft();
            }
        } else {
            // prioritize vertical direction
            if (attackDirection.y > 0) {
                swordAttack.AttackUp();
            } else {
                swordAttack.AttackDown();
            }
        }
    }

    public void EndSwordAttack() {
        // called in animation event at end of attack animation
        UnlockMovement();
        swordAttack.StopAttack();
    }

}
