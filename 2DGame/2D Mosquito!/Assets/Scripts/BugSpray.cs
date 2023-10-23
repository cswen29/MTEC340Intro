using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BugSpray : MonoBehaviour
{
    public GameObject spray;
    //public Transform sprayPoint;
    //public float sprayStrength = 2.0f;

    private SpriteRenderer spraySpriteRenderer;
    public SpriteRenderer sprayRenderer;

    public Transform sprayDetectionArea;
    public float detectionDelay = 0.1f;
    public LayerMask enemyLayer;

    private float lastDetectionTime;
    private bool isSpraying = false;

    AudioSource _source; 
    [SerializeField] AudioClip bugSpray;

    private void Start()
    {
        // Initialise the spray sprite renderer within Spray
        spraySpriteRenderer = sprayRenderer;
        spraySpriteRenderer.enabled = false;

        _source = GetComponent<AudioSource>(); 

        //sprayPoint = GameObject.Find("Spray").transform;

        lastDetectionTime = Time.time;
    }

    private void Update()
    {
        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSpraying = true;
                _source.PlayOneShot(bugSpray);
                _source.loop = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isSpraying = false;
                spraySpriteRenderer.enabled = false;
                _source.Stop();
            }

            if (isSpraying)
            {
                spraySpriteRenderer.enabled = true;
                if (Time.time - lastDetectionTime > detectionDelay)
                {
                    lastDetectionTime = Time.time;
                    DetectAndDestroyEnemies();
                }
            }
        }
    }

    //public void Spray()
    //{
    //    spraySpriteRenderer.enabled = true;

    //    if (Time.time - lastDetectionTime > detectionDelay)
    //    {
    //        lastDetectionTime = Time.time;
    //        DetectAndDestroyEnemies();
    //    }
    //    //GameObject bugspray = Instantiate(spray, sprayPoint.position, sprayPoint.rotation);
    //    //spray.GetComponent<Rigidbody2D>().AddForce(sprayPoint.up * sprayStrength, ForceMode2D.Impulse);
    //}

    private void DetectAndDestroyEnemies()
    {
        Bounds detectionBounds = sprayDetectionArea.GetComponent < SpriteRenderer>().bounds;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(detectionBounds.center, detectionBounds.size, 0, enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            Destroy(enemyCollider.gameObject);
        }
    }

    //private void DestroySelf()
    //{
    //    Invoke("Respawn", 5);

    //}

    //public void Respawn()
    //{
    //    GameObject enemyClone = (GameObject)Instantiate(enemyRef);
    //    enemyClone.transform.position = transform.position;
    //    Destroy(gameObject);
    //}
    //private void OnTriggerEnter2D(Collider2D other)
    //{

    //    //    if (other.CompareTag("Enemy"))
    //    //    {
    //    //        float distance = Vector2.Distance(transform.position, other.transform.position);
    //    //        if (distance <= sprayRange)
    //    //        {
    //    //            Debug.Log("Ouch!");
    //    //            Destroy(other.gameObject);
    //    //        }
    //}
    //}

}
