using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpecialAICutscene : MonoBehaviour
{
    public GameObject thePlayer;

    Animator playerAnimator;

    Rigidbody playerRigidbody;

    RigidbodyConstraints originalConstraints;
    
    GameObject timeline;

    PlayableDirector timelineDirector;

    void Start()
    {
        playerAnimator = thePlayer.GetComponent<Animator>();
        playerRigidbody = thePlayer.GetComponent<Rigidbody>();
        originalConstraints = playerRigidbody.constraints;
        timeline = GameObject.Find("Timeline");
        timelineDirector = timeline.GetComponent<PlayableDirector>();
    }

    public void SpecialAIEnters()
    {
        timelineDirector.Play();
        playerAnimator.enabled = false;
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(3);
        timelineDirector.Stop();
        playerRigidbody.constraints = originalConstraints;
        playerAnimator.enabled = true;
    }
}
