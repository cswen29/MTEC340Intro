using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    // The gravity scale to apply when inside the cube
    public float flippedGravity = -1.0f;
    private Vector3 originalGravity;
    private bool gravityFlipped = false;

    private void Start()
    {
        originalGravity = Physics.gravity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gravityFlipped && other.CompareTag("Player")) // Assuming the player has the "Player" tag
        {
            Debug.Log("Player Detected");
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // Reverse the actual gravity direction for the scene
                Physics.gravity *= flippedGravity;
                Debug.Log("Gravity flipped");
                // Set the player's velocity to zero to prevent falling
                playerRigidbody.velocity = Vector3.zero;
                // Disable the player's gravity to prevent falling
                playerRigidbody.useGravity = false;
                gravityFlipped = true; // Set the flag to prevent repeated triggering
            }
        }
    }
}
