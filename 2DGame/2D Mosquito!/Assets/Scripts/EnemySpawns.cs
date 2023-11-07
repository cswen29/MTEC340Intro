using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemySpawns : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnpointY;
    //[SerializeField] TextMeshProUGUI levelText;
    private EnemySpawns _enemySpawns; // Declaration


    //private float timer;
    //private float maxTimer;
    private GameObject currentEnemy; // Reference to the current enemy
    private GameObject originalEnemyPrefab; // Reference to the original enemy
    private int enemiesKilled = 0;
    private int enemiesToSpawn = 1;


    private void Start()
    {
        originalEnemyPrefab = Instantiate(enemyPrefab); // Create an instance of the original prefab
        originalEnemyPrefab.SetActive(false); // Disable the original prefab

        StartCoroutine("SpawnEnemyTimer");
    }

    public void SetEnemySpawns(EnemySpawns enemySpawns)
    {
        _enemySpawns = enemySpawns;
    }

    void SpawnEnemy()
    {
        if (enemiesKilled >= enemiesToSpawn)
        {
            enemiesToSpawn *= 2;
            enemiesKilled = 0;
        }

        float y = spawnpointY;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), y, 0));
        spawnPoint.z = 0;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            InstantiateEnemy(spawnPoint);
        }

    }


    IEnumerator SpawnEnemyTimer()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameBehaviour.Instance.GameState == GameBehaviour.State.Play && currentEnemy == null);
            SpawnEnemy();
        }

            //yield return new WaitForSeconds(1.0f); // Adjust the interval if needed
        
        //if (timer >= maxTimer)
        //{
        //    SpawnEnemy();
        //    timer = 0;
        //    maxTimer = Random.Range(5f, 10f);
        //}

        //timer += 0.1f;
        //yield return new WaitForSeconds(6.0f);
    }

    public void DisableOriginalEnemy()
    {
        originalEnemyPrefab.SetActive(false);
    }

    //New things Nov 6 midnight:
    void InstantiateEnemy(Vector3 spawnPoint)
    {
        currentEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

    }
    //void UpdateLevelUI(int level)
    //{
    //    if (levelText != null)
    //    {
    //        levelText.text = "Level: " + level;
    //    }
    //}

    //public GameObject enemyPrefab;
    //public int maxEnemies = 4;
    //public float respawnTime = 3.0f;

    //private GameObject[] enemyInstances;

    //private void Start()
    //{
    //    enemyInstances = new GameObject[maxEnemies];
    //    for (int i = 0; i < maxEnemies; i++)
    //    {
    //        SpawnEnemy(i);
    //    }
    //}

    //private void SpawnEnemy(int index)
    //{
    //    Vector3 spawnPosition = GetRandomSpawnPosition();
    //    enemyInstances[index] = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    //}

    //private Vector3 GetRandomSpawnPosition()
    //{
    //    return new Vector3(Random.Range(-5, 7), Random.Range(-8, 5), 0);
    //}

    //public void RespawnEnemy(int maxEnemies)
    //{
    //    Vector3 spawnPosition = GetRandomSpawnPosition();
    //    enemyInstances[maxEnemies] = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);


    //        //StartCoroutine(RespawnAfterDelay(destroyedEnemy));


    //    //IEnumerator RespawnAfterDelay(GameObject destroyedEnemy)
    //    //{
    //    //    yield return new WaitForSeconds(respawnTime);
    //    //    // Find an available slot and respawn the enemy
    //    //    for (int i = 0; i < maxEnemies; i++)
    //    //    {
    //    //        if (enemyInstances[i] == null)
    //    //        {
    //    //            enemyInstances[i] = Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
    //    //        }
    //    //    }
    //    //}
    //}
}


//using System.Collections;
//using UnityEngine;

//public class EnemySpawn : MonoBehaviour
//{
//    //    [SerializeField] GameObject associatedMosquitos;
//    //    public Transform SpawnPoint;

//    [SerializeField] GameObject[] associatedMosquitos; // Array to hold different mosquito types
//    public GameObject[] SpawnPoints; // Reference to your Spawn Point game objects
//    public int selectedSpawnIndex = 0; // Variable to choose the spawn index

//    private int numberOfMosquitoes = 1; // Initial number of mosquitoes to spawn
//    private int mosquitoesKilled = 0;
//    private bool isSpawning = true;

//    private void Start()
//    {
//        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
//        {
//            StartCoroutine(SpawnMosquitoes());
//        }
//    }

//    private IEnumerator SpawnMosquitoes()
//    {
//        while (isSpawning)
//        {
//            for (int i = 0; i < SpawnPoints.Length; i++)
//            {
//                SpawnEnemy(selectedSpawnIndex);
//            }

//            yield return new WaitForSeconds(1.0f); // Adjust this value to control the spawn rate
//        }
//    }

//    // Call this method when a mosquito is killed
//    public void MosquitoKilled()
//    {
//        mosquitoesKilled++;

//        Debug.Log("Mosquito deaths: " + mosquitoesKilled);
//        // Increase the number of mosquitoes spawned per kill
//        numberOfMosquitoes *= 2;

//        // Respawn more mosquitoes based on the updated count
//        for (int i = 0; i < numberOfMosquitoes; i++)
//        {
//            SpawnEnemy(selectedSpawnIndex);
//        }
//    }

//    //void SpawnEnemy()
//    //{
//    //    Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, 0)); //Convert to Camera pos
//    //    spawnPoint.z = 0;
//    //    GameObject.Instantiate(associatedMosquitos, spawnPoint, Quaternion.identity);

//    //}
//    void SpawnEnemy(int spawnIndex)
//    {
//        if (spawnIndex >= 0 && spawnIndex < associatedMosquitos.Length && spawnIndex < SpawnPoints.Length)
//        {
//            GameObject mosquitoToSpawn = associatedMosquitos[spawnIndex];
//            GameObject spawnPoint = SpawnPoints[spawnIndex];

//            if (mosquitoToSpawn != null && spawnPoint != null)
//            {
//                GameObject.Instantiate(mosquitoToSpawn, spawnPoint.transform.position, Quaternion.identity);
//            }
//            else
//            {
//                Debug.LogError("Mosquito or Spawn Point not assigned for index: " + spawnIndex);
//            }
//        }
//        else
//        {
//            Debug.LogError("Invalid index for mosquito or spawn point: " + spawnIndex);
//        }
//    }




// AHAHDAFHADHFAKSDJFHASKJDFHAKJSDFHAJKSDFHAJKDHFAJKDSHFJAKDSF





    //IEnumerator SpawnEnemyTimer()
    //{
    //    if (timer >= maxTimer)
    //    {
    //        SpawnEnemy();
    //        timer = 0;
    //        maxTimer = Random.Range(5f, 20f);
    //    }

    //    timer += 0.1f;
    //    yield return new WaitForSeconds(4.0f);
    //}




    //public GameObject enemyPrefab;
    //public int maxEnemies = 4;
    //public float respawnTime = 3.0f;

    //private GameObject[] enemyInstances;

    //private void Start()
    //{
    //    enemyInstances = new GameObject[maxEnemies];
    //    for (int i = 0; i < maxEnemies; i++)
    //    {
    //        SpawnEnemy(i);
    //    }
    //}

    //private void SpawnEnemy(int index)
    //{
    //    Vector3 spawnPosition = GetRandomSpawnPosition();
    //    enemyInstances[index] = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    //}

    //private Vector3 GetRandomSpawnPosition()
    //{
    //    return new Vector3(Random.Range(-5, 7), Random.Range(-8, 5), 0);
    //}

    //public void RespawnEnemy(int maxEnemies)
    //{
    //    Vector3 spawnPosition = GetRandomSpawnPosition();
    //    enemyInstances[maxEnemies] = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);


    //        //StartCoroutine(RespawnAfterDelay(destroyedEnemy));


    //    //IEnumerator RespawnAfterDelay(GameObject destroyedEnemy)
    //    //{
    //    //    yield return new WaitForSeconds(respawnTime);
    //    //    // Find an available slot and respawn the enemy
    //    //    for (int i = 0; i < maxEnemies; i++)
    //    //    {
    //    //        if (enemyInstances[i] == null)
    //    //        {
    //    //            enemyInstances[i] = Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
    //    //        }
    //    //    }
    //    //}
    //}

