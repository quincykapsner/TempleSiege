using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public bool canMove = true;
    public bool canAttack = true;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    // List of game over messages
    private List<string> gameOverMessages = new List<string>
    {
        "Her magic cannot reach beyond the temple.",
        "Were you listening when she mentioned the barrier?", 
        "There aren't any secrets out there, just death.",
        "You have abandoned your goddess."
    };

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        // if player is allowed to move (like when not attacking)
        if (canMove) {
            // if movement is not 0
            if (movementInput != Vector2.zero) {
                // set lastHorizontal and lastVertical 
                animator.SetFloat(lastHorizontal, movementInput.x);
                animator.SetFloat(lastVertical, movementInput.y);

                // try to move
                bool success = TryMove(movementInput);
                if (!success) {
                    // if movement was not successful, try to move on the X axis
                    success = TryMove(new Vector2(movementInput.x, 0));
                    if (!success) {
                        // if x movement was not successful, try to move on the Y axis
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }

                // set the animator values
                animator.SetFloat(horizontal, movementInput.x);
                animator.SetFloat(vertical, movementInput.y);
            } else {
                // if no movement, set the animator values to 0
                animator.SetFloat(horizontal, 0);
                animator.SetFloat(vertical, 0);
            }
        } else {
            // if player is not allowed to move, set the animator values to 0
            animator.SetFloat(horizontal, 0);
            animator.SetFloat(vertical, 0);
        }
    }

    // ============ movement ============

    private bool TryMove(Vector2 direction) {
        // check for potential collisions
            int count = rb.Cast(
            direction, // X and Y values from -1 to 1 that represent the direction from the body to look for collisions
            movementFilter, // settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // list of collisions to store the found collisions into after Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset // amount to cast = movement + offset
        );

        if (count == 0) {
            // if no collisions, move the player
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>(); 
    }

    // ========= sword attack stuff =========

    void OnFire() {
        if (canAttack) {
            canAttack = false;
            animator.SetTrigger("SwordAttack");
        }
    }

    public void SwordAttack() {
        // called in animation event at start of attack animation
        LockMovement();

        // determine attack direction based on dominant axis
        float absHorizontal = Mathf.Abs(animator.GetFloat(lastHorizontal));
        float absVertical = Mathf.Abs(animator.GetFloat(lastVertical));
        if (absHorizontal > absVertical) {
            // prioritize horizontal direction
            if (animator.GetFloat(lastHorizontal) > 0) {
                swordAttack.AttackRight();
            } else {
                swordAttack.AttackLeft();
            }
        } else {
            // prioritize vertical direction
            if (animator.GetFloat(lastVertical) > 0) {
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
        canAttack = true;
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }

    // ========= player death ========= 

    public void Death() {
        // must be separate from defeated to allow for animations
        animator.SetTrigger("Defeated");
    }

    public void Defeated() {
        // called in animation event
        canMove = false; // disable player movement
        string message = gameOverMessages[Random.Range(0, gameOverMessages.Count)];
        FindObjectOfType<GameController>().GameOver(message); // show game over screen
    }
}
