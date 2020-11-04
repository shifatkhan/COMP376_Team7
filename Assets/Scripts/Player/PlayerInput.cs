using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 directionalInput { get; private set; }
    public bool jumpInput { get; private set; } // TODO: Remove. Testing instantaneous inputs.

    bool holdingObject = false;

    GameObject currentObjectHold;
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
            //Hold object
            //Press 'f' to to pickup closest object
            if(Input.GetButtonDown("PickDrop") && objectNear.transform.tag == "Mop" && holdingObject == false)
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
            
            //Destroy object
            if(objectNear.transform.tag == "Puddle")
            {
                //Presses 'x' to clean spill with mop
                if(Input.GetButtonDown("Clean") && currentObjectHold != null && currentObjectHold.transform.tag == "Mop")
                {
                    currentObjectHold.GetComponent<AudioSource>().Play();
                    Destroy(objectNear.gameObject);
                } 
            }
        }



        

    }
}
