using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check if the collision was against the player.
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();

            if (player != null)
            {
                // Toggle the speed boost power-up
                Debug.Log("Picked up Speed!!");
                StartCoroutine(player.ActivateSpeedBoost());  
            }

            Destroy(gameObject);
        }
    }


}
