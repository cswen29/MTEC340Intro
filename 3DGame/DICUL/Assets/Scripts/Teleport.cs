using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerGO;
    [SerializeField] private float teleportationTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke(nameof(Teleportation), teleportationTime);
        }
    }

    private void Teleportation()
    {
        Debug.Log("Sending..!");
        playerGO.SetActive(false);
        player.position = destination.position;
        playerGO.SetActive(true);
    }

}
