using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public Vector3 directionalInput { get; private set; }
    public static bool enableMovement = true;

    public bool jumpInput { get; private set; }
    public bool interactInput { get; private set; } = false;
    public bool pickDropInput { get; private set; } = false;
    public bool forceStart { get; private set; } = false;
    public bool forceHold { get; private set; } = false;
    public bool forceLaunch { get; private set; } = false;
    public bool forceCancel { get; private set; } = false;

    public bool throwHold { get; private set; } = false;
    public bool throwRelease { get; private set; } = false;
    public static bool enableThrow = true;

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
        if (enableMovement)
            directionalInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;
        else
            directionalInput = Vector3.zero;

        // JUMP
        jumpInput = Input.GetButtonDown("Jump");
        interactInput = Input.GetButtonDown("Interact");
        pickDropInput = Input.GetButtonDown("PickDrop");

        //Force/throw with left_mouse button
        // forceStart = Input.GetMouseButtonDown(0);
        // forceHold = Input.GetMouseButton(0);
        // forceLaunch = Input.GetMouseButtonUp(0);
        // forceCancel = Input.GetMouseButtonDown(1);

        //Throw with spacebar
        forceStart = Input.GetButtonDown("ThrowHold");
        forceHold = Input.GetButton("ThrowHold");
        forceLaunch = Input.GetButtonUp("ThrowHold");
    }
}
