using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

// Place object in a given component bin. For future use 
[AddComponentMenu("Control Script/FPS Input")]

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float _speed = 6.0f;
    [SerializeField] float _gravity;
    public float runSpeed = 10.0f;


    private CharacterController _controller;
    //private Rigidbody _rb;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        //_rb = GetComponent<Rigidbody>(); 
        
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;

        Vector3 movement = new(deltaX, 0, deltaZ);

        // Clamp diagonal movement
        movement = Vector3.ClampMagnitude(movement, _speed);

        // Apply gravity after X and Z have been clamped
        movement.y = _gravity;

        movement *= Time.deltaTime;

        // Convert movement vector to rotation settings of player
        movement = transform.TransformDirection(movement);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Apply the movement with the run speed
            _controller.Move(runSpeed * Time.deltaTime * movement);
        }
        else
        {
            _controller.Move(_speed * Time.deltaTime * movement);
        }

        // Convert movement vector to rotation settings of player
        movement = transform.TransformDirection(movement);

        _controller.Move(movement);
    }
}
