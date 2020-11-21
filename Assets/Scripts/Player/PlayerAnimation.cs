using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //private PlayerInput playerInput;
    private PlayerInputManager playerInput;

    Animator playerAnimator;
    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        playerInput = PlayerInputManager.instance;
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
