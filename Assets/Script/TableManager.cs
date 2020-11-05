using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//- Have a TableManager script: This will randomly assign clients to a table (we dont need to show the client models
//for now. We can just make the table change color to show that it is occupied)

//[SerializeField]
//private float spawnRate = 2f; // AKA Something will happen every 2 seconds

//// This variable represents the last Time you spawned something
//// So if you spawned something at 20 seconds, this will be equal to 20 seconds.
//// Time.time is the in-game counter, so the 20 seconds mentioned above is when Time.time = 20s
//private float spawnTime = 0f;

//void Start()
//{
//    spawnTime = Time.time; // If you ever enable the gameobject in the middle of the game.
//}

//void Update()
//{
//    // Check if we should spawn.
//    if (Time.time > spawnTime)
//    {
//        spawnTime += ghostSpawnRate;

//        int i = random
//            if (table[i] has no customers)
//                Enable customers
//        }

//}


//Where EnableCustomers can be a function that changes the color of that table
public class TableManager : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public GameObject[] tables;

    public bool isOccupied;                    //If the table already has a group on it

    //private float time;                             //current time
    private float spawnTime = 0f;

    [SerializeField]
    private float spawnRate = 2f;



    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        
        spawnTime = Time.time;
        isOccupied = false;

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        
        // Check if we should spawn.
        if (Time.time > spawnTime)
        {
            spawnTime += spawnRate;

            int i = Random.Range(0, tables.Length);
            Debug.Log(tables[i]);
            if (isOccupied == false)
            {
                tables[i].GetComponent<Table>().EnableCustomers();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

}


