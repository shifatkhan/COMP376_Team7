using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopJames : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private GameObject cleaningPrefab;

    //public GameObject Mop;

    //public BoxCollider player;
    //public BoxCollider mop;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //CheckHasMop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puddle"))
        {
            // TODO: Usually we check if the mop is in the player's hand, but what if the player throws the mop on a puddle?
            //      Also, instead of waiting 2 seconds before destroying, instantiate a particle onDestroy.
            Destroy(other.gameObject);
            audioSource.Play();
            Instantiate(cleaningPrefab, transform.position, transform.rotation);
        }
    }

    //void CheckHasMop()
    //{

    //    if (Mop.transform.parent != null && Mop.transform.parent.name == "PickupObject")
    //    {
    //        Debug.Log("Mop's Parent: " + Mop.transform.parent.name);
       
    //        //mop = player.GetComponent<BoxCollider>();
            
    //    }
      
     
    //}

   
}
