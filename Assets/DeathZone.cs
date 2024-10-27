using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            //GameObject player = collision.gameObject;
            PlayerController playerController = collision.GetComponent<PlayerController>();
            playerController.Death();
            //Debug.Log("Player has died!");
        }
    }
}
