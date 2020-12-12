using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //private PlayerInput playerInput;
    private PlayerInputManager playerInput;
    private CheckNearbyInteraction nearbyObject;
    Animator playerAnimator;
    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        playerInput = PlayerInputManager.instance;
        playerAnimator = GetComponent<Animator>();
        nearbyObject = GetComponent<CheckNearbyInteraction>();
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
            playerAnimator.SetLayerWeight(2, 0);
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

    public void ObjectDropped()
    {
        playerAnimator.SetLayerWeight(2, 0);
        playerAnimator.SetBool("isMopping", false);
    }

    public void FoodPickedAnimation()
    {
        playerAnimator.SetLayerWeight(2, 1);
        playerAnimator.SetBool("isPickedUp", true);
        playerAnimator.SetBool("isHolding", false);
    }

    public void FoodDroppedAnimation()
    {
        playerAnimator.SetBool("isPickedUp", false);
        playerAnimator.SetBool("isMopping", false);
    }

    public void MopAnimation()
    {
        playerAnimator.SetLayerWeight(2, 1);
        playerAnimator.SetBool("isMopping", true);
        playerAnimator.SetBool("isHolding", false);
    }

    public void PlayWalkSound()
    {
        AudioManager.PlayFootstepAudio();
    }
}
