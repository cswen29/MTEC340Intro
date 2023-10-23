using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBehaviour : MonoBehaviour
{
    public int _health, _maxHealth = 3;
    [SerializeField] float PlayerSpeed = 6.0f;
    public float XLimit = 6.2f;
    public float YLimit = 5.0f;
    private int collisionCount = 0;
    public int maxCollisions = 3; // Set to 3 for 3 collisions
    SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;
    //private Material matRed;
    //private Material matDefault;


    Vector2 mousePosition;

    [SerializeField] KeyCode Front;
    [SerializeField] KeyCode Back;
    [SerializeField] KeyCode Left;
    [SerializeField] KeyCode Right;

    private void Start()
    {
        _health = _maxHealth;
        //matRed = Resources.Load("RedFlash", typeof(Material)) as Material; 
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
        {

            if (collision.gameObject.CompareTag("Enemy"))
            {
                collisionCount++;
                //spriteRenderer.material = matRed;
                if (collisionCount >= maxCollisions)
                {
                    PlayerDestruction();
                }
            }
        }
    }

    public void PlayerDestruction()
    {
        _health--;
        if (_health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("EndScene");
        }

    }

    void Update()
    {
        if (GameBehaviour.Instance.GameState == GameBehaviour.State.Play)
        {
            //Take in WASD, 4 way movements 
            if (Input.GetKey(Front) && transform.position.y < YLimit)
            {
                transform.position += new Vector3(0, PlayerSpeed, 0) * Time.deltaTime;
            }
            else if (Input.GetKey(Back) && transform.position.y > -YLimit)
            {
                transform.position -= new Vector3(0, PlayerSpeed, 0) * Time.deltaTime;
            }

            if (Input.GetKey(Right) && transform.position.x < XLimit)
            {
                transform.position += new Vector3(PlayerSpeed, 0, 0) * Time.deltaTime;
            }
            else if (Input.GetKey(Left) && transform.position.x > -XLimit)
            {
                transform.position -= new Vector3(PlayerSpeed, 0, 0) * Time.deltaTime;
            }

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
    }

    private void FixedUpdate()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle; 
    }
}
