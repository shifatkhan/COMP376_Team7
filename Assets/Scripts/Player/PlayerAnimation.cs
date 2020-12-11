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
            playerAnimator.SetBool("isLaunched", false);
            playerAnimator.SetBool("isWalking", false);
        }

        if(playerInput.forceStart)
        {
            playerAnimator.SetBool("isHolding", true);
            playerAnimator.SetLayerWeight(1, 1);
        }

        if(playerInput.forceLaunch)
        {
            playerAnimator.SetBool("isLaunched", true);
            playerAnimator.SetBool("isHolding", false);
        }
    }

    public void FinishThrow()
    {
        playerAnimator.SetLayerWeight(1, 0);
    }
}
