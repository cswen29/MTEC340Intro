using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int damageAmount = 10;
    public float _maxHealth = 3.0f;
    public float _currentHealth;
    [SerializeField] float _enemyspeed = 5.0f;
    public int points = 1; // Points awarded for killing this enemy

    Transform _target;
    Rigidbody2D _rb;
    Vector2 _moveDirection;
    public EnemySpawns _enemySpawns;
    //public GameBehaviour gameBehaviour;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Initialise health 
        _currentHealth = _maxHealth;
        _target = GameObject.Find("Player").transform;

        // Find and set the reference to the EnemySpawns components in the scene
        EnemySpawns[] enemySpawns = FindObjectsOfType<EnemySpawns>();

        if (enemySpawns.Length > 0)
        {
            _enemySpawns = enemySpawns[0]; // Assign the first found EnemySpawns
            SetEnemySpawnsForAll(_enemySpawns);
        }
        else
        {
            Debug.LogError("No EnemySpawns found!");
        }

        //// Assign the correct EnemySpawns to _enemySpawns based on the GameObject name or other criteria
        //foreach (EnemySpawns enemySpawn in enemySpawns)
        //{
        //    if (enemySpawn.gameObject.name == "Spawnpoint_Mosquito")
        //    {
        //        _enemySpawns = enemySpawn;
        //    }
        //    if (enemySpawn.gameObject.name == "Spawnpoint_Mosquito1")
        //    {
        //        _enemySpawns = enemySpawn;
        //    }
        //    if (enemySpawn.gameObject.name == "Spawnpoint_Mosquito2")
        //    {
        //        _enemySpawns = enemySpawn;
        //    }
        //}

        //if (_enemySpawns == null)
        //{
        //    Debug.LogError("No EnemySpawns found!");
        //}

        //SetEnemySpawns(_enemySpawns);
    }

    public void SetEnemySpawnsForAll(EnemySpawns enemySpawns)
    {
        EnemySpawns[] allEnemySpawns = FindObjectsOfType<EnemySpawns>();

        foreach (EnemySpawns enemySpawn in allEnemySpawns)
        {
            enemySpawn.SetEnemySpawns(enemySpawns);
        }
    }


    void Update()
    {
        //Taken from Bmo on Youtube, this ensures the enemy heads towards the direction of where the target is
        //Normalising will cap it at a maximum of 1
        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
        {
            if (_target)
            {
                Vector3 direction = (_target.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _rb.rotation = angle;
                _moveDirection = direction;
            }
            //if (_currentHealth <= 0)
            //{
            //    Destroy(gameObject);
            //}
        }
    }

    public void TakeDamageFromBugSpray(int damage)
    {
        // Logic to handle enemy's damage from Bug Spray
        _currentHealth -= damage; // Reduce the enemy's health by the Bug Spray damage

        if (_currentHealth <= 0)
        {
            _enemySpawns.EnemyKilled();
            GameBehaviour.Instance.UpdateScore(points);
            Destroy(gameObject); // Destroy the enemy when health reaches zero
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(damageAmount);
        }
    }

    private void FixedUpdate()
    {
        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
        {
            if (_target)
            {
                _rb.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * _enemyspeed;
            }
        }
        else if (GameBehaviour.Instance.GameState == GameBehaviour.State.Pause)
        {
            // Make sure enemies stop
            _rb.velocity = Vector2.zero;
        }
    }
}
