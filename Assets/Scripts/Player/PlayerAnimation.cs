using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerInput playerInput;

    Animator playerAnimator;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update() 
    {
        //Walking animation when player is walking
        if(playerInput.directionalInput != Vector3.zero)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        //Idle animation when player is not walking
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }
}
