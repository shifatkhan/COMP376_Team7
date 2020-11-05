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

    void Update()
    {
        // MOVE - remove 'normalized' for analog movements.
        directionalInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

        // JUMP
        jumpInput = Input.GetButtonDown("Jump");

        //Nearby objects within a radius of 2
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 2);

        foreach (Collider objectNear in nearbyObjects)
        {
            //Press 'f' to to pickup closest object
            if(Input.GetButtonDown("PickDrop") && objectNear.transform.tag == "Mop" && holdingObject == false) //Should remove the tag == mop, and make it so that the player can hold any pickupble object
            {
                objectNear.GetComponent<PickUp>().PickObjectUp();
                currentObjectHold = objectNear.gameObject;
                holdingObject = true;
            } 
            
            //Presses 'f' to to drop object
            else if(Input.GetButtonDown("PickDrop") && holdingObject == true)
            {
                currentObjectHold.GetComponent<PickUp>().PlaceObjectDown();
                holdingObject = false;
                currentObjectHold = null;
            } 

            //Near water spill
            if(objectNear.transform.tag == "Puddle")
            {
                //Start timer for button hold
                if(Input.GetButtonDown("Clean"))
                {
                    startTime = Time.deltaTime;
                }

                //Presses 'x' to clean spill with mop
                if(Input.GetButton("Clean") && currentObjectHold != null && currentObjectHold.transform.tag == "Mop")
                {
                    timer += Time.deltaTime;
                    if(startTime + holdButtonTime <= timer)
                    {
                        currentObjectHold.GetComponent<AudioSource>().Play();

                        //Show bubbly effect and destroy objects after 2 second
                        objectNear.GetComponentInChildren<ParticleSystem>().Play();
                        Destroy(objectNear.gameObject, 2);

                        //Reset timer
                        timer = 0;
                    }
                } 
            }
        }



        

    }
}
