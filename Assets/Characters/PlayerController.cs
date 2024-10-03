using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        // if movement is not 0, try to move
        if (movementInput != Vector2.zero) {
            bool success = TryMove(movementInput);

            if (!success) {
                // if movement was not successful, try to move on the X axis
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success) {
                    // if x movement was not successful, try to move on the Y axis
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

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
}
