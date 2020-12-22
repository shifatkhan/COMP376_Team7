using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : MonoBehaviour
{
    public AudioSource audioSource { get; private set; }

    public GameObject bubblePrefab;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puddle"))
        {
            Destroy(other.gameObject);
            //audioSource.Play();
            Instantiate(bubblePrefab, transform.position, transform.rotation);
        }
    }
}
