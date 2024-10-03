using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Vector2 movementInput;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        // if movement input isnt 0, try to move
        if (movementInput != Vector2.zero) {
            bool success = TryMove(movementInput);

            if (!success){
                // if movement fails, try to move in the x direction
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success) {
                    // if x movement fails, try to move in the y direction
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        // check for collisions
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collision
            movementFilter, // settings that determine where a collision can occur on such as layers to collide w
            castCollisions, // list of collisions to store the found collisions into after Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset // amount to cast = movement + offset
        );

        if (count == 0) {
            // if no collisions, move
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
