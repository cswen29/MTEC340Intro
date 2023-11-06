//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(AudioSource))]

//public class BugSpray : MonoBehaviour
//{
//    public GameObject spray;

//    private SpriteRenderer spraySpriteRenderer;
//    public SpriteRenderer sprayRenderer;

//    public Transform sprayDetectionArea;
//    public float detectionDelay = 0.1f;
//    public LayerMask enemyLayer;

//    //private float lastDetectionTime;
//    private bool isSpraying = false;
//    private bool isRecharging = false;
//    private bool isButtonPressed = false; // Track button state

//    //private float currentSprayDuration = 0f; // Track spray duration???
//    public float maxSprayDuration = 5.0f; // Define the maximum spray duration


//    AudioSource _source; 
//    [SerializeField] AudioClip bugSpray;

//    private void Start()
//    {
//        // Initialise the spray sprite renderer within Spray
//        spraySpriteRenderer = sprayRenderer;
//        spraySpriteRenderer.enabled = false;

//        _source = GetComponent<AudioSource>(); 

//        //sprayPoint = GameObject.Find("Spray").transform;

//        //lastDetectionTime = Time.time;
//    }

//    private void Update()
//    {
//        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
//        {
//            // Check for spraying input
//            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !isRecharging)
//            {
//                isButtonPressed = true;
//                StartCoroutine(SprayDuration());
//            }
//            else if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)))
//            {
//                isButtonPressed = false;
//            }

//            if (isButtonPressed && !isRecharging)
//            {
//                isSpraying = true;
//            }
//            else
//            {
//                isSpraying = false;
//                sprayRenderer.enabled = false;
//                _source.Stop();
//            }

//            // Disabling the spray when not spraying
//            if (!isSpraying && sprayRenderer.enabled)
//            {
//                sprayRenderer.enabled = false;
//                _source.Stop();
//            }
//        }
//    }


//    private void DetectAndDestroyEnemies()
//    {
//        Bounds detectionBounds = sprayDetectionArea.GetComponent<SpriteRenderer>().bounds;

//        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(detectionBounds.center, detectionBounds.size, 0, enemyLayer);

//        foreach (var enemyCollider in hitEnemies)
//        {
//            enemyCollider.GetComponent<EnemyBehaviour>().TakeDamageFromBugSpray(5);
//        }

//        //StartCoroutine(SprayRecharge());
//    }

//    //private IEnumerator SprayRecharge()
//    //{
//    //    isRecharging = true;
//    //    yield return new WaitForSeconds(3f); // Spray duration
//    //    isRecharging = false;
//    //    yield return new WaitForSeconds(1f); // Recharge duration
//    //}

//    private IEnumerator SprayDuration()
//    {
//        isSpraying = true;
//        sprayRenderer.enabled = true;
//        _source.Play();

//        float startTime = Time.time; // Get the start time

//        while (isSpraying)
//        {
//            float elapsedTime = Time.time - startTime; // Calculate the elapsed time

//            if (elapsedTime >= maxSprayDuration)
//            {
//                isSpraying = false;
//            }
//            else
//            {
//                Debug.Log("Spray and kill the mosquitos!!");
//                DetectAndDestroyEnemies();
//                yield return null;
//            }
//        }

//        isRecharging = true;
//        Debug.Log("Recharging...");
//        sprayRenderer.enabled = false;
//        _source.Stop();
//        yield return new WaitForSeconds(2.0f); // Recharge duration
//        isRecharging = false;
//    }

//    //public void Spray()
//    //{
//    //    spraySpriteRenderer.enabled = true;

//    //    if (Time.time - lastDetectionTime > detectionDelay)
//    //    {
//    //        lastDetectionTime = Time.time;
//    //        DetectAndDestroyEnemies();
//    //    }
//    //    //GameObject bugspray = Instantiate(spray, sprayPoint.position, sprayPoint.rotation);
//    //    //spray.GetComponent<Rigidbody2D>().AddForce(sprayPoint.up * sprayStrength, ForceMode2D.Impulse);
//    //}



//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class NewBugSpray : MonoBehaviour
{
    public GameObject spray;

    private SpriteRenderer spraySpriteRenderer;
    public SpriteRenderer sprayRenderer;

    public Transform sprayDetectionArea;
    public float detectionDelay = 0.1f;
    public LayerMask enemyLayer;

    private bool isSpraying = false;
    private bool isRecharging = false;
    public float maxSprayDuration = 5.0f; // Define the maximum spray duration
    public float rechargeDuration = 2.0f; // Define the recharge duration
    private bool isButtonPressed = false; // Track button state

    AudioSource _source;
    [SerializeField] AudioClip bugSpray;

    private void Start()
    {
        // Initialise the spray sprite renderer within Spray
        spraySpriteRenderer = sprayRenderer;
        spraySpriteRenderer.enabled = false;

        _source = GetComponent<AudioSource>();
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

    //private void Update()
    //{
    //    if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
    //    {
    //        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && !isRecharging)
    //        {
    //            isSpraying = true;
    //            isButtonPressed = true; 

    //            if (isButtonPressed && !isRecharging && sprayRenderer.enabled == false)
    //            {
    //                StartCoroutine(Spray());
    //            }
    //        }
    //        else
    //        {
    //            isSpraying = false;
    //            sprayRenderer.enabled = false;
    //            isButtonPressed = false;
    //            _source.Stop();
    //        }

    //        if (!isSpraying && sprayRenderer.enabled)
    //        {
    //            StopSpraying();
    //        }
    //    }
    //}

    //private void StartSpraying()
    //{
    //    sprayRenderer.enabled = true;
    //    DetectAndDestroyEnemies();
    //    _source.Play();
    //}

    //private void StopSpraying()
    //{
    //    sprayRenderer.enabled = false;
    //    _source.Stop();
    //}

    //private IEnumerator Spray()
    //{
    //    isSpraying = true;
    //    isRecharging = true;
    //    StartSpraying();

    //    yield return new WaitForSeconds(maxSprayDuration);

    //    StopSpraying();

    //    yield return new WaitForSeconds(rechargeDuration);

    //    isRecharging = false;
    //}

    private void DetectAndDestroyEnemies()
    {
        Bounds detectionBounds = sprayDetectionArea.GetComponent<SpriteRenderer>().bounds;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(detectionBounds.center, detectionBounds.size, 0, enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            enemyCollider.GetComponent<EnemyBehaviour>().TakeDamageFromBugSpray(5);
        }
    }
}
