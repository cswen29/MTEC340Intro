using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public int _maxBloodLevel = 100;
    public int _currentBloodLevel;

    public BloodBar bloodBar;
    [SerializeField] float PlayerSpeed = 6.0f;
    public float XLimit = 60.0f;
    public float YLimit = 45.0f;
    SpriteRenderer spriteRenderer;


    //Power Up Speed Boost
    private bool isSpeedBoostActive = false;
    private float normalSpeed;
    private float speedBoostAmount = 6.0f; // Increase speed

    //private float speedBoostDuration = 3.0f; // Duration of speed boost in seconds


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
        _currentBloodLevel = _maxBloodLevel;
        bloodBar.SetMaxBlood(_maxBloodLevel);
        normalSpeed = PlayerSpeed;

        // Check if bloodBar is assigned before using it
        if (bloodBar != null)
        {
            bloodBar.SetMaxBlood(_maxBloodLevel);
        }
        else
        {
            Debug.LogError("BloodBar is not assigned to PlayerBehaviour!");
        }
    
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Ouch, Mosquito bit me!");
        _currentBloodLevel -= damage;

        bloodBar.SetBlood(_currentBloodLevel);

        if (_currentBloodLevel <= 0)
        {
            GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

            Destroy(gameObject);

            if (cameraObject != null)
            {
                Destroy(cameraObject);
            }

            GameBehaviour.Instance.GameOver();
        }
    }



    public void ToggleIncreaseBlood(int extraBloodAmount)
    {
        if (_currentBloodLevel + extraBloodAmount <= _maxBloodLevel)
        {
            _currentBloodLevel += extraBloodAmount;
        }
        else
        {
            int difference = _currentBloodLevel + extraBloodAmount - _maxBloodLevel;

            // Deduct the extra from current blood level teehee
            _currentBloodLevel -= difference;
        }

        bloodBar.SetBlood(_currentBloodLevel);
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

    public IEnumerator ActivateSpeedBoost()
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            normalSpeed = PlayerSpeed;
            Debug.Log("This is the " + normalSpeed + " and " + PlayerSpeed);
            PlayerSpeed += speedBoostAmount; // Increase player's speed

            yield return new WaitForSeconds(2); 
            
            PlayerSpeed = normalSpeed; // Revert player's speed back to normal
            isSpeedBoostActive = false; // Reset the flag
            Debug.Log("Back to Normal Speed: " + PlayerSpeed);
            
        }
    }

    private void FixedUpdate()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle; 
    }
}
