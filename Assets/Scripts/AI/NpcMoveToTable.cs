using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class NpcMoveToTable : MonoBehaviour
{

    public int tableNumber;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;


    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.speed = 2.5f;
        agent.radius = 0.4f;
    }

    private void Start()
    {
        EnableWalkAnimation();
    }

    void Update()
    {
    }

    public void DisableAIMovement()
    {
        agent.isStopped = true;
        agent.enabled = false;
    }

    public void SetTableNumber(int tableNumber)
    {
        this.tableNumber = tableNumber;
        SetAgentTableDestination();
    }

    void SetAgentTableDestination()
    {
        Table[] tables = TableManager.Instance.tables;
        agent.destination = tables[tableNumber].transform.position;
    }

    public void EnableWalkAnimation()
    {
        animator.SetBool("walking", true);
        animator.SetBool("sitting", false);
    }

    public void EnableSittingAnimation()
    {
        animator.SetBool("walking", false);
        animator.SetBool("sitting", true);
    }
}
