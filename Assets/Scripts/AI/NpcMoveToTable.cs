using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class NpcMoveToTable : MonoBehaviour
{

    public int tableNumber;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    public GameObject idle;
    public GameObject walking;
    public GameObject sitting;


    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
    }

    void Update()
    {
    }

    public void DisableAIMovement()
    {
        agent.isStopped = true;
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

    public void EnableSitAnimation()
    {
        idle.SetActive(false);
        walking.SetActive(false);
        sitting.SetActive(true);
        
    }

    public void EnableWalkAnimation()
    {
        animator.SetBool("walking", true);
        animator.SetBool("sitting", false);
    }
}
