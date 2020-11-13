using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMove : MonoBehaviour
{

    [SerializeField]
    Transform destination:

    // Represents the table the agent will walk and sit to
    [SerializeField]
    int tableNumber;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        SetTableNumber();

        Vector3 target = destination.transform.position;
        agent.SetDestination(target);



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetTableNumber()
    {
        tableNumber = Random.Range(1, 13);
    }
}
