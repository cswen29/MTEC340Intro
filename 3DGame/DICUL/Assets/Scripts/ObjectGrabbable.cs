using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody _rb;
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        _rb.useGravity = false;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        _rb.useGravity = true;

    }
    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            //Smoothing
            float lerpSpeed = 20.0f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            _rb.MovePosition(newPosition); 
        }
    }
}
