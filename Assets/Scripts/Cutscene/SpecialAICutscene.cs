using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAICutscene : MonoBehaviour
{
    public GameObject thePlayer;

    PlayerMovement playerMovement;
    Animator playerAnimator;

    Rigidbody playerRigidbody;

    public GameObject mainCamera;
    public GameObject cutsceneCam;

    RigidbodyConstraints originalConstraints;

    void Start()
    {
        playerMovement = thePlayer.GetComponent<PlayerMovement>();
        playerAnimator = thePlayer.GetComponent<Animator>();
        playerRigidbody = thePlayer.GetComponent<Rigidbody>();
        originalConstraints = playerRigidbody.constraints;
    }

    public void SpecialAIEnters()
    {
        print("AI");
        cutsceneCam.SetActive(true);
        mainCamera.SetActive(false);
        //playerMovement.enabled = false;
        playerAnimator.enabled = false;
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(3);
        mainCamera.SetActive(true);
        playerRigidbody.constraints = originalConstraints;
        //playerMovement.enabled = true;
        playerAnimator.enabled = true;
        cutsceneCam.SetActive(false);
    }
}
