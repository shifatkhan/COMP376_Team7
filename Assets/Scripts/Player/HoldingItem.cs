using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingItem : MonoBehaviour
{
    public GameObject Mop;

    void OnTriggerEnter(Collider other)
    {
        if (Mop.transform.parent != null && Mop.transform.parent.name == "PickupObject")
        {
            Debug.Log("Mop's Parent: " + Mop.transform.parent.name);
            if (other.CompareTag("Puddle"))
            {
                // TODO: Usually we check if the mop is in the player's hand, but what if the player throws the mop on a puddle?
                //      Also, instead of waiting 2 seconds before destroying, instantiate a particle onDestroy.
               
                Destroy(other.gameObject);
            }
        }
       
    }

    // TODO: Check item to see which current item the player is holding
    void checkItem()
    {
       
    }
}
