using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puddle"))
        {
            // TODO: Usually we check if the mop is in the player's hand, but what if the player throws the mop on a puddle?
            //      Also, instead of waiting 2 seconds before destroying, instantiate a particle onDestroy.
            Destroy(other.gameObject);
            audioSource.Play();
        }
    }
}
