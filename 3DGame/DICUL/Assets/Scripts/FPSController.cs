using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]

public class FPSController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float walkSpeed = 8.0f;
    [SerializeField] float runSpeed = 16.0f;
    [SerializeField] float jumpPower = 7.0f;
    [SerializeField] float _gravity = 10.0f;

    //public float gravity = 10.0f;

    [Header("Player Look")]
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    [SerializeField] GameObject crosshair;
    private bool crosshairVisible = true;

    CharacterController characterController;
    private Rigidbody rigidBody;

    [Header("Player Step Climb")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    //[SerializeField] float stepHeight = 0.3f;
    //[SerializeField] float stepSmooth = 0.1f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>(); 
        //stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        #region Handles Movement

        //float deltaX = Input.GetAxis("Horizontal") * walkSpeed;
        //float deltaZ = Input.GetAxis("Vertical") * walkSpeed;

        //Vector3 movement = new(deltaX, 0, deltaZ);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        //movement = Vector3.ClampMagnitude(movement, walkSpeed);
        //movement.y = _gravity;



        // Run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= _gravity * Time.deltaTime;
        }
        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        }
        #endregion

        // Toggle Crosshair
        if (Input.GetKeyDown(KeyCode.C))
        {
            crosshairVisible = !crosshairVisible;
            crosshair.SetActive(crosshairVisible);
        }

    }

    //void FixedUpdate()
    //{
    //    StepClimb();
    //}


    //void StepClimb()
    //{
    //    RaycastHit hitLower;
    //    if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
    //    {
    //        // Make sure object you want to step on isn't tall enough 
    //        RaycastHit hitUpper;
    //        if (Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
    //        {
    //            rigidbody.position -= new Vector3(0f, -stepSmooth, 0f);
    //        }
    //    }

    //    RaycastHit hitLower45;
    //    if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
    //    {
    //        RaycastHit hitUpper45;
    //        if (Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
    //        {
    //            rigidbody.position -= new Vector3(0f, -stepSmooth, 0f);
    //        }

    //    }

    //    RaycastHit hitLowerMinus45;
    //    if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
    //    {
    //        RaycastHit hitUpperMinus45;
    //        if (Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
    //        {
    //            rigidbody.position -= new Vector3(0f, -stepSmooth, 0f);
    //        }

    //    }


}
