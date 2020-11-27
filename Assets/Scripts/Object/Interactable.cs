using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this on any object to make it interactable.
/// TODO: Support "press & hold"
/// 
/// @author ShifatKhan
/// </summary>
[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    protected bool playerInRange { get; private set; }

    [Header ("Interactable vars")]
    public bool interacted;
    [SerializeField] protected GameEvent interactEvent;

    protected PlayerInputManager playerInput; 

    public virtual void Start()
    {
        playerInRange = false;

        playerInput = PlayerInputManager.instance;
    }

    public virtual void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            OnInteract();
        }
    }

    public virtual void OnInteract()
    {
        // Check if there's an Event to raise or not.
        if (interactEvent)
            interactEvent.Raise();

        interacted = true;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interacted = false;
        }
    }
}
