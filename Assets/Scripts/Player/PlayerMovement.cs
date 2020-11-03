using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player stats")]// TODO: Replace this with a ScriptableObject.
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 3.5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float turnSpeed = 50f;
    private float movementY; // Calculates Y velocity and movement.

    private CharacterController characterController;
    private PlayerInput playerInput;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        UpdateFaceDirection();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // Add move speed.
        Vector3 movement = playerInput.directionalInput * moveSpeed;

        // Calculate gravity.
        movementY -= gravity * Time.deltaTime;
        movement.y = movementY;

        // Move.
        characterController.Move(movement * Time.deltaTime);
    }

    // TODO: remove jump
    private void Jump()
    {
        if (playerInput.jumpInput && characterController.isGrounded)
        {
            movementY = jumpForce;
        }
    }

    private void UpdateFaceDirection()
    {
        if (playerInput.directionalInput.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(playerInput.directionalInput), turnSpeed * Time.deltaTime);
        }
    }
}
