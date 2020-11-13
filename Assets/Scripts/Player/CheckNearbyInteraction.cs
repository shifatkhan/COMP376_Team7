using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearbyInteraction : MonoBehaviour
{
    bool holdingObject = false;
    GameObject currentObjectHold; //Reference to current object being hold

    public void NearbyObjects()
    {
        if(holdingObject == false)
        {
            //Nearby objects within a radius of 2
            Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 2);

            if(nearbyObjects.Length > 0)
            {
                // Check the nearest object if there are more than 1.
                Collider nearest = nearbyObjects[0];
                foreach (Collider objectNear in nearbyObjects)
                {
                    if(objectNear.GetComponent<PickUp>() != null && Vector3.Distance(transform.position, objectNear.transform.position) < Vector3.Distance(transform.position, nearest.transform.position))
                    {
                        nearest = objectNear;
                    }
                }

                //Picks up the object
                PickUp pickUpScript = nearest.GetComponent<PickUp>();
                if(pickUpScript != null) //Object must contain PickUp script
                {
                    pickUpScript.PickObjectUp();
                    currentObjectHold = nearest.gameObject;
                    holdingObject = true;
                }

            }
        }
        //Drop object
        else
        {
            currentObjectHold.GetComponent<PickUp>().PlaceObjectDown();
            holdingObject = false;
            currentObjectHold = null;
        }
    }
}
