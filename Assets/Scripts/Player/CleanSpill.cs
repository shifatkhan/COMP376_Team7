using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanSpill : MonoBehaviour
{
    public void CleanWaterSpill()
    {
        Debug.Log("Water Spill");

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
