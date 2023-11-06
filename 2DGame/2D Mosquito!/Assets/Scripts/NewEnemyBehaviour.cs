using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyBehaviour : MonoBehaviour
{
    public int damageAmount = 10;
    public float _currentHealth;
    public float _maxHealth = 3.0f;
    [SerializeField] float _enemyspeed = 5.0f;

    Transform _target;
    Rigidbody2D _rb;
    Vector2 _moveDirection;

    //private EnemySpawn _enemySpawn;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Initialise health 
        _currentHealth = _maxHealth;
        _target = GameObject.Find("Player").transform;
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
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
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
