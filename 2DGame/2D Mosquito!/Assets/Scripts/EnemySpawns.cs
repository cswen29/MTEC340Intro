using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    [SerializeField] GameObject enemy;
    [SerializeField] float Spawnpoint;


    private void Start()
    {
        timer = 0;
        maxTimer = Random.Range(5f, 12f);
    }

    private void Update()
    {
        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
        {
            StartCoroutine("SpawnEnemyTimer");
        }

    }

    void SpawnEnemy()
    {
        float y = Spawnpoint;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), y, 0)); //Convert to Camera pos
        spawnPoint.z = 0;
        GameObject.Instantiate(enemy, spawnPoint, new Quaternion(0, 0, 0, 0));
    }

    IEnumerator SpawnEnemyTimer()
    {
        if (timer >= maxTimer)
        {
            SpawnEnemy();
            timer = 0;
            maxTimer = Random.Range(5f, 20f);
        }

        timer += 0.1f;
        yield return new WaitForSeconds(4.0f);
    }




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
