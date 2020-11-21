using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMoveToTable : MonoBehaviour
{

    public int tableNumber;
    UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //SetAgentTableDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableAIMovement()
    {
        agent.enabled = false;
    }

    public void SetTableNumber(int tableNumber)
    {
        this.tableNumber = tableNumber;
        SetAgentTableDestination();
    }

    void SetAgentTableDestination()
    {
        Table[] tables = GameObject.Find("TableManager").GetComponent<TableManager>().tables;
        agent.destination = tables[tableNumber].transform.position;
    }
}
