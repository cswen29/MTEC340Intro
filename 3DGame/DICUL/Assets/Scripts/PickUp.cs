using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Transform _cameraTransform;
    //[SerializeField] Transform _itemGrabPointTransform;
    [SerializeField] Transform objectGrabPointTransform; 
    [SerializeField] LayerMask _pickUpLayerMask;

    private ObjectGrabbable objectGrabbable;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                float pickUpDistance = 3.3f;
                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, _pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        Debug.Log("Grabbing");
                        objectGrabbable.Grab(objectGrabPointTransform); 
                    }
                }
            }
            else
            {
                Debug.Log("Dropping"); 
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
            
        }
    }
}
