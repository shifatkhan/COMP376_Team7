using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform objectPosition;
    Rigidbody objectRigidBody;
    BoxCollider objectBoxCollider;

    public bool pickedUp { get; private set; }

    private void Awake()
    {
        objectPosition = GameObject.FindGameObjectWithTag("Player").transform.Find("PickupObject").transform;
        objectRigidBody = GetComponent<Rigidbody>();
        objectBoxCollider = GetComponent<BoxCollider>();
    }

    public void PickObjectUp()
    {
        objectBoxCollider.enabled = false;
        objectRigidBody.useGravity = false; //No gravity on object when holding it

        //Free rotation and position, so it stops moving when picked
        objectRigidBody.freezeRotation = true; 
        objectRigidBody.constraints = RigidbodyConstraints.FreezePosition;
        
        this.transform.position = objectPosition.position;
        this.transform.parent = objectPosition;
        this.transform.localRotation = Quaternion.identity; //object is held upright
        this.transform.localPosition = Vector3.zero;

        pickedUp = true;
    }

    public void PlaceObjectDown()
    {
        this.transform.parent = null;
        objectBoxCollider.enabled = true;
        objectRigidBody.useGravity = true;
        objectRigidBody.freezeRotation = false; 
        objectRigidBody.constraints = RigidbodyConstraints.None;

        pickedUp = false;
    }
}
