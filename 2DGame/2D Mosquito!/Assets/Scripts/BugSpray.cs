using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BugSpray : MonoBehaviour
{
    public GameObject spray;

    private SpriteRenderer spraySpriteRenderer;
    public SpriteRenderer sprayRenderer;

    public Transform sprayDetectionArea;
    public float detectionDelay = 0.1f;
    public LayerMask enemyLayer;

    //private float lastDetectionTime;
    private bool isSpraying = false;
    private bool isRecharging = false;
    private bool isButtonPressed = false; // Track button state

    //private float currentSprayDuration = 0f; // Track spray duration???
    public float maxSprayDuration = 5.0f; // Define the maximum spray duration


    AudioSource _source;
    [SerializeField] AudioClip bugSpray;

    private void Start()
    {
        // Initialise the spray sprite renderer within Spray
        spraySpriteRenderer = sprayRenderer;
        spraySpriteRenderer.enabled = false;

        _source = GetComponent<AudioSource>();

        //sprayPoint = GameObject.Find("Spray").transform;

        //lastDetectionTime = Time.time;
    }

    private void Update()
    {
        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
        {
            // Check for spraying input
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !isRecharging)
            {
                isButtonPressed = true;
                StartCoroutine(SprayDuration());
            }
            else if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)))
            {
                isButtonPressed = false;
            }

            if (isButtonPressed && !isRecharging)
            {
                isSpraying = true;
            }
            else
            {
                isSpraying = false;
                sprayRenderer.enabled = false;
                _source.Stop();
            }

            // Disabling the spray when not spraying
            if (!isSpraying && sprayRenderer.enabled)
            {
                sprayRenderer.enabled = false;
                _source.Stop();
            }
        }
    }


    private void DetectAndDestroyEnemies()
    {
        Bounds detectionBounds = sprayDetectionArea.GetComponent<SpriteRenderer>().bounds;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(detectionBounds.center, detectionBounds.size, 0, enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            enemyCollider.GetComponent<EnemyBehaviour>().TakeDamageFromBugSpray(5);
        }

    }



    private IEnumerator SprayDuration()
    {
        isSpraying = true;
        sprayRenderer.enabled = true;
        _source.Play();

        float startTime = Time.time; // Get the start time

        while (isSpraying)
        {
            float elapsedTime = Time.time - startTime; // Calculate the elapsed time

            if (elapsedTime >= maxSprayDuration)
            {
                isSpraying = false;
            }
            else
            {
                DetectAndDestroyEnemies();
                yield return null;
            }
        }

        isRecharging = true;
        Debug.Log("Recharging...");
        sprayRenderer.enabled = false;
        _source.Stop();
        yield return new WaitForSeconds(2.0f); // Recharge duration
        isRecharging = false;
    }




}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
//public class BugSpray : MonoBehaviour
//{
//    public GameObject spray;
//    public SpriteRenderer sprayRenderer;
//    public Transform sprayDetectionArea;
//    public LayerMask enemyLayer;
//    public float maxSprayDuration = 5.0f;
//    public float minRechargeDuration = 1.0f;
//    public float maxRechargeDuration = 5.0f;

//    private bool isSpraying = false;
//    private bool isButtonPressed = false;

//    private AudioSource _source;
//    private float currentSprayDuration = 0f;

//    private void Start()
//    {
//        _source = GetComponent<AudioSource>();
//        sprayRenderer.enabled = false;
//    }

//    private void Update()
//    {
//        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
//        {
//            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !isSpraying)
//            {
//                isButtonPressed = true;
//                StartCoroutine(Spray());
//            }
//            else if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)))
//            {
//                isButtonPressed = false;
//            }

//            if (isButtonPressed && !isSpraying)
//            {
//                StartSpraying();
//            }
//            else if (!isButtonPressed && isSpraying)
//            {
//                StopSpraying();
//            }
//        }
//    }

//    private void StartSpraying()
//    {
//        isSpraying = true;
//        sprayRenderer.enabled = true;
//        _source.Play();
//    }

//    private void StopSpraying()
//    {
//        isSpraying = false;
//        sprayRenderer.enabled = false;
//        _source.Stop();
//        StartCoroutine(Recharge());
//    }

//    private IEnumerator Spray()
//    {
//        currentSprayDuration = 0f;

//        while (currentSprayDuration < maxSprayDuration)
//        {
//            DetectAndDestroyEnemies();
//            currentSprayDuration += Time.deltaTime;
//            yield return null;
//        }

//        StopSpraying();
//    }

//    private IEnumerator Recharge()
//    {
//        float rechargeDuration = Mathf.Lerp(minRechargeDuration, maxRechargeDuration, currentSprayDuration / maxSprayDuration);
//        yield return new WaitForSeconds(rechargeDuration);
//    }

//    private void DetectAndDestroyEnemies()
//    {
//        Bounds detectionBounds = sprayDetectionArea.GetComponent<SpriteRenderer>().bounds;

//        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(detectionBounds.center, detectionBounds.size, 0, enemyLayer);

//        foreach (var enemyCollider in hitEnemies)
//        {
//            enemyCollider.GetComponent<EnemyBehaviour>().TakeDamageFromBugSpray(5);
//        }
//    }
//}
