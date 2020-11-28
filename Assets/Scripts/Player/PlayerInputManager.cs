using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public Vector3 directionalInput { get; private set; }
    public bool jumpInput { get; private set; } // TODO: Remove. Testing instantaneous inputs.
    public bool interactInput { get; private set; } = false;

    public bool pickDropInput { get; private set; } = false;

<<<<<<< HEAD
    public bool throwHold { get; private set; } = false;
    public bool throwRelease { get; private set; } = false;

=======
>>>>>>> 040f9bddee212c26e4baa559d86b520b901ed06d
    // Singleton Pattern
    public static PlayerInputManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }

        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // MOVE - remove 'normalized' for analog movements.
        directionalInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

        // JUMP
        jumpInput = Input.GetButtonDown("Jump");
        interactInput = Input.GetButtonDown("Interact");
        pickDropInput = Input.GetButtonDown("PickDrop");

        throwHold = Input.GetButtonDown("ThrowHold");
        throwRelease = Input.GetButtonDown("ThrowRelease");
    }
}
