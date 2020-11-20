using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is only responsible for the player's movements.
/// 
/// @author ShifatKhan
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player stats")]// TODO: Replace this with a ScriptableObject.
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 50f;

    [SerializeField] private bool slipping = false;
    [SerializeField] private float slipDuration = 3f;

    //private PlayerInput playerInput;

    private Rigidbody rb;
    private PlayerInputManager playerInput;
    Animator playerAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //playerInput = GetComponent<PlayerInput>();
        //playerInput = PlayerInputManager.instance;
        playerAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        playerInput = PlayerInputManager.instance;
        //playerInput = GetComponent<PlayerInput>();
        //rb = GetComponent<Rigidbody>();
        //playerAnimator = GetComponent<Animator>();

        slipping = false;
    }

    private void Update()
    {
        //UpdateFaceDirection();
        //Move();
    }

    private void FixedUpdate()
    {
        Move();
        UpdateFaceDirection();
    }

    /// <summary>
    /// Move the player according to the world space.
    /// </summary>
    private void Move()
    {
        if (!slipping)
        {
            // Move player by explicitly changing its velocity.
            rb.velocity = playerInput.directionalInput * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Move player using forces so it slips like ice.
            rb.AddForce(playerInput.directionalInput * moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Rotate the player towards the direction it is moving.
    /// </summary>
    private void UpdateFaceDirection()
    {
        if (playerInput.directionalInput.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(playerInput.directionalInput), turnSpeed * Time.deltaTime);
        }
    }

    private IEnumerator SlipCo()
    {
        slipping = true;

        yield return new WaitForSeconds(slipDuration);

        slipping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puddle"))
        {
            StartCoroutine(SlipCo());
        }
    }
}
