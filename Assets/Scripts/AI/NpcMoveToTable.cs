using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMoveToTable : MonoBehaviour
{

    int tableNumber;
    UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetAgentTableDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAgentTableDestination()
    {
        GameObject[] tables = GameObject.FindGameObjectsWithTag("Table");
        tableNumber = Random.Range(0, tables.Length - 1);
        agent.destination = tables[tableNumber].transform.position;
    }
}
