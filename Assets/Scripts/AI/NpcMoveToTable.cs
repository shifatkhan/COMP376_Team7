using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class NpcMoveToTable : MonoBehaviour
{

    public int tableNumber;
    private UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
        Table[] tables = TableManagerJames.Instance.tables;
        agent.destination = tables[tableNumber].transform.position;
    }
}
