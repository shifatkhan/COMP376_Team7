using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearbyInteraction : MonoBehaviour
{
    bool holdingObject = false;
    GameObject currentObjectHold; //Reference to current object being held
    PlayerInputManager playerInput;

    Collider previousNearestObject;

    Collider nearest;

    Collider currentOutlined;

    float radius = 3.5f;

    private void Start()
    {
        playerInput = PlayerInputManager.instance;
    }

    void Update() 
    {
        NearbyObjects();  
    }

    void NearbyObjects()
    {
        //Nearby objects within a radius of 3.5
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, radius);

        //Calculate nearest object
        nearest = nearbyObjects[0];
        foreach (Collider objectNear in nearbyObjects)
        {
            if(objectNear.GetComponent<PickUp>() != null && Vector3.Distance(transform.position, objectNear.transform.position) < Vector3.Distance(transform.position, nearest.transform.position))
            {
                nearest = objectNear;
            }
        }

        //If nearest object contains Outline script then highlight it
        if(holdingObject == false && nearest.transform.gameObject.GetComponent<Outline>() != null) 
        {
            currentOutlined = nearest; 
            currentOutlined.transform.gameObject.GetComponent<Outline>().enabled = true;

            if(previousNearestObject == null)
            {
                previousNearestObject = currentOutlined; 
            }
        }

        turnOffOutline(); //Checks if the highlight of the object should be turned off

        //Picks up/drops down the object
        pickDropCheck();
    }

    void turnOffOutline()
    {
        //Case 1: object goes too far away, so do not highlight it anymore
        if(Vector3.Distance(transform.position, nearest.transform.gameObject.transform.position) > radius)
        {
            if(currentOutlined != null)
                currentOutlined.transform.gameObject.GetComponent<Outline>().enabled = false;
            previousNearestObject = currentOutlined;   
        }
        
        //Case 2: another object becomes closer, so unhighlight the object that was already highlighted
        if((previousNearestObject != nearest) && (previousNearestObject != null))
        {
            if(previousNearestObject.transform.gameObject.GetComponent<Outline>() != null)
                previousNearestObject.transform.gameObject.GetComponent<Outline>().enabled = false;
            previousNearestObject = currentOutlined;
        }
    }

    //Users presses 'f' to pick up or drop down an object
    void pickDropCheck()
    {
        if(playerInput.pickDropInput == true)
        {
            if(holdingObject == false)
            {
                PickUp pickUpScript = nearest.GetComponent<PickUp>();
                if(pickUpScript != null) //Object must contain PickUp script
                {
                    pickUpScript.PickObjectUp();
                    currentObjectHold = nearest.gameObject;
                    holdingObject = true;
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

    //Returns current object held
    public GameObject getHeldObject()
    {
        return currentObjectHold;
    }
}
