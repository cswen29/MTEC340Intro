using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerGO;
    [SerializeField] private float teleportationTime;

    [Header("Teleport Particle")]
    [SerializeField] ParticleSystem teleportParticle = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(" Player Detected");
            TeleportParticles();
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

    public void TeleportParticles()
    {
        //Play teleporting particles
        teleportParticle.Play();

        //Play teleporting sound 
    }

}

