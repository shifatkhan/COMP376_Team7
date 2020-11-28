using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class CustomerAnimation : MonoBehaviour
{
    private Animator anim;

    private UnityEngine.AI.NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update animation
        if (!agent.isStopped)
        {
            // Walking
            anim.SetBool("isWalking", true);
        }
        else
        {
            // Idle
            anim.SetBool("isWalking", false);
        }
    }
}
