using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenPickUps : MonoBehaviour
{
    //[SerializeField] AudioClip collectStinger;
    //[SerializeField] GameObject particleEffectPrefab; // Particle effect to play on collection
    public static int collectedTokens = 0;

    private void Awake()
    {
        collectedTokens = 0; 
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Wwise implementation

            collectedTokens += 1;

            //// Play token collect sound
            //if (collectStinger != null)
            //{
            //    //AudioSource.PlayClipAtPoint(collectStinger, transform.position);
            //}

            //// Instantiate a particle effect at the sphere's position
            //if (particleEffectPrefab != null)
            //{
            //    Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            //}

            gameObject.SetActive(false); 
        }
    }

    void Update()
    {
        // Make the sphere spin or rotate
        transform.Rotate(Vector3.up * Time.deltaTime * 50.0f, Space.World);

        // Make the sphere glow (example: change the sphere's material emission color)
        // You can manipulate the material's color or shader properties to create a glowing effect
        // Example:
        // Renderer renderer = GetComponent<Renderer>();
        // Material material = renderer.material;
        // Color newColor = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
        // material.SetColor("_EmissionColor", newColor);
    }
}

