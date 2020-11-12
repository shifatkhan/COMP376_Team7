using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearbyInteraction : MonoBehaviour
{
    bool holdingObject = false;
    GameObject currentObjectHold; //Reference to current object being hold
    public LayerMask pickupLayer; // TODO: move to different pickup script.

    public void NearbyObjects()
    {
        if(holdingObject == false)
        {
            //Nearby objects within a radius of 2
            //Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 2, pickupLayer); //If we don't use layers to know if an object is pickuble, then delete this line
            Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 2);

            if(nearbyObjects.Length > 0)
            {
                // Check the nearest object if there are more than 1.
                Collider nearest = nearbyObjects[0];

                foreach (Collider objectNear in nearbyObjects)
                {
                    //Check if object contains PickUp script
                    PickUp script = objectNear.GetComponent<PickUp>();

                    if(script != null && Vector3.Distance(transform.position, objectNear.transform.position) < Vector3.Distance(transform.position, nearest.transform.position))
                    {
                        nearest = objectNear;
                    }
                }

                nearest.GetComponent<PickUp>().PickObjectUp();
                currentObjectHold = nearest.gameObject;
                holdingObject = true;
            }
        }
        else
        {
            currentObjectHold.GetComponent<PickUp>().PlaceObjectDown();
            holdingObject = false;
            currentObjectHold = null;
        }
    }
}
