using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMoveToTable : MonoBehaviour
{

    Vector3[] tablePositions = new Vector3[13];
    int tableNumber;

    UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        tablePositions[0] = new Vector3(-9.19999981f,0f,0.629999995f);
        tablePositions[1] = new Vector3(-9.19999981f,0f,-7.55000019f);
        tablePositions[2] = new Vector3(-9.19999981f,0f,-13.9099998f);
        tablePositions[3] = new Vector3(-0.899999976f,0f,-16.8400002f);
        tablePositions[4] = new Vector3(15.3599997f,0f,-16.8400002f);
        tablePositions[5] = new Vector3(-1.15713441f,0f,-10.1233044f);
        tablePositions[6] = new Vector3(6.16286564f,0f,-10.1233044f);
        tablePositions[7] = new Vector3(13f,0f,-10.1232996f);
        tablePositions[8] = new Vector3(13f,0f,-5.4768033f);
        tablePositions[9] = new Vector3(11.3999996f,0f,3.83999991f);
        tablePositions[10] = new Vector3(7.28000021f,0f,-17.8299999f);
        tablePositions[11] = new Vector3(2.46000004f,-0.209999993f,-4.63999987f);
        tablePositions[12] = new Vector3(22.0100002f,0f,-16.2900009f);

        SetTableNumber();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetTableNumber()
    {
        tableNumber = Random.Range(0,12);
        print(tableNumber);
        agent.destination = tablePositions[tableNumber];

        print(agent.destination);
    }
}
