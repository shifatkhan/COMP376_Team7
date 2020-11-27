using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: Delete this file.
[System.Obsolete("This class is getting replaced by PlayerInputManager.cs (use GameManager-James gameobject)", true)]
public class PlayerInput : MonoBehaviour
{
    public Vector3 directionalInput { get; private set; }
    public bool jumpInput { get; private set; } // TODO: Remove. Testing instantaneous inputs.
    public bool interactInput { get; private set; } = false;
    public bool pickDropInput { get; private set; } = false;

    void Update()
    {
        // MOVE - remove 'normalized' for analog movements.
        directionalInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

        // JUMP
        jumpInput = Input.GetButtonDown("Jump");

        // INTERACT (with tables etc)
        interactInput = Input.GetButtonDown("Interact");

        pickDropInput = Input.GetButtonDown("PickDrop");
    }
}
