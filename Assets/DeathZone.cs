using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    /*
    public Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    */

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            GameObject player = collision.gameObject;
            if (player != null) {
                // player death animation, game over screen, etc.
                Debug.Log("Player has died!");
            }
        }
    }
}
