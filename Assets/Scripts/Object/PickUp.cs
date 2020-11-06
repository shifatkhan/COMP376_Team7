using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform objectPosition;

    private void Awake()
    {
        objectPosition = GameObject.FindGameObjectWithTag("Player").transform.Find("PickupObject").transform;
    }

    public void PickObjectUp()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false; //No gravity on object when holding it

        //Free rotation and position, so it stops moving when picked
        GetComponent<Rigidbody>().freezeRotation = true; 
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        
        this.transform.position = objectPosition.position;
        this.transform.parent = objectPosition;
        this.transform.localRotation = Quaternion.identity; //object is held upright
        this.transform.localPosition = Vector3.zero;
    }

    public void PlaceObjectDown()
    {
        this.transform.parent = null;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false; 
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
