using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc2 : Enemy
{
    public GameObject statue;
    public Collider2D hitboxCollider; // collider for hitbox

    public SwordAttack swordAttack;
    private Coroutine attackCoroutine; // coroutine for attacking statue

    private Animator animator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    // Start is called before the first frame update
    public override void Start()
    { 
        base.Start();
        health = 2f;
        statue = GameObject.FindGameObjectWithTag("Statue");
        hitboxCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        // if allowed to move
        if (canMove) {
            Vector2 direction = Vector2.zero;

            if (statue != null) {
                // if statue exists, move towards statue
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

    void OnCollisionEnter2D(Collision2D collision) {
        // this triggers when enemy hitbox collides with statue
        if (collision.collider.CompareTag("Statue")) { 
            // stop movement
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
        // this triggers when enemy hitbox no longer collides with statue
        if (collision.collider.CompareTag("Statue")) { 
            UnlockMovement();
            if (attackCoroutine != null) {
                StopCoroutine(attackCoroutine); // stop attacking when leaving the statu
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
        animator.SetTrigger("SwordAttack");
    }

    public void SwordAttack() {
        // called in animation event at start of attack animation
        LockMovement();

        // determine attack direction based on direction to statue
        Vector2 attackDirection = (statue.transform.position - transform.position).normalized;
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
