using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 directionalInput { get; private set; }
    public bool jumpInput { get; private set; } // TODO: Remove. Testing instantaneous inputs.

    bool holdingObject = false;

    float startTime = 0f; //Timer for button holding
    float holdButtonTime = 2f; //Hold button for 3 seconds
    float timer = 0f;

    GameObject currentObjectHold; //Reference to current object being hold

    //public LayerMask pickupLayer; // TODO: move to different pickup script.

    void Update()
    {
        // MOVE - remove 'normalized' for analog movements.
        directionalInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

        // JUMP
        jumpInput = Input.GetButtonDown("Jump");

        // -------------------------- TODO: Move to another Pickup script --------------------------- //

        if (Input.GetButtonDown("Interact"))
        {
            // if(holdingObject == false)
            // {
            //     //Nearby objects within a radius of 2
            //     Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 2, pickupLayer);

            //     if(nearbyObjects.Length > 0)
            //     {
            //         // Check the nearest object if there are more than 1.
            //         Collider nearest = nearbyObjects[0];

            //         foreach (Collider objectNear in nearbyObjects)
            //         {
            //             if(Vector3.Distance(transform.position, objectNear.transform.position) < Vector3.Distance(transform.position, nearest.transform.position))
            //             {
            //                 nearest = objectNear;
            //             }
            //         }

            //         nearest.GetComponent<PickUp>().PickObjectUp();
            //         currentObjectHold = nearest.gameObject;
            //         holdingObject = true;
            //     }
            // }
            // else
            // {
            //     currentObjectHold.GetComponent<PickUp>().PlaceObjectDown();
            //     holdingObject = false;
            //     currentObjectHold = null;
            // }

            GetComponent<CheckNearbyInteraction>().NearbyObjects();
        }

        if (Input.GetButtonDown("Clean"))
        {
            GetComponent<CleanSpill>().CleanWaterSpill();
        }
        // Collider[] puddles = Physics.OverlapSphere(transform.position, 2);


        // foreach (Collider objectNear in puddles)
        // {
        //     //Near water spill
        //     if (objectNear.transform.tag == "Puddle")
        //     {
        //         Debug.Log("Puddle");
        //         //Start timer for button hold
        //         if (Input.GetButtonDown("Clean"))
        //         {
        //             startTime = Time.deltaTime;
        //         }

        //         //Presses 'x' to clean spill with mop
        //         if (Input.GetButton("Clean") && currentObjectHold != null && currentObjectHold.transform.tag == "Mop")
        //         {
        //             timer += Time.deltaTime;
        //             if (startTime + holdButtonTime <= timer)
        //             {
        //                 currentObjectHold.GetComponent<AudioSource>().Play();

        //                 //Show bubbly effect and destroy objects after 2 second
        //                 objectNear.GetComponentInChildren<ParticleSystem>().Play();
        //                 Destroy(objectNear.gameObject, 2);

        //                 //Reset timer
        //                 timer = 0;
        //             }
        //         }
        //     }
        // }
    }
}
