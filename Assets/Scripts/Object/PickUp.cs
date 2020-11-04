using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform theDest;

    public void PickObjectUp()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false; //No gravity on object when holding it
        this.transform.position = theDest.position;
        this.transform.parent = GameObject.Find("PickupDest").transform;
        this.transform.localRotation = Quaternion.identity; //object is held upright
        this.transform.localPosition = Vector3.zero;
    }

    public void PlaceObjectDown()
    {
        this.transform.parent = null;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
