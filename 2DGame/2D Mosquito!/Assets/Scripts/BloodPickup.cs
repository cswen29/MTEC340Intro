using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check if the collision was against the player.
        if (collision.CompareTag("Player"))
        {
            // Trigger the Player's power up.
            collision.gameObject.GetComponent<PlayerBehaviour>().ToggleIncreaseBlood(10);

            // Destroy the coin after it has been picked up.
            Destroy(gameObject);
        }
    }
}
