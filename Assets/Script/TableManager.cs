using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//- Have a TableManager script: This will randomly assign clients to a table (we dont need to show the client models
//for now. We can just make the table change color to show that it is occupied)

//Hmm so what I think we should do is have an Array of GameObjects[] called tables
//We will populate this array in the inspector by order of the table numbers (each table will have a unique number).
//Then, inside the TableManager script, you can do something like (in pseudo code)
//Update(){
//    every Random.range(0, maxInterval) seconds{
//        int i = Random.range(0, tables.size);
//        tables[i].EnableCustomers(): 
//  }
//}


//Where EnableCustomers can be a function that changes the color of that table
public class TableManager : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public GameObject[] tables;

    private bool seated = false;                    //If the table already has a group on it
    //private float timer = 30;                        //Timers when customers are seated

    public float maxTime = 15;
    public float minTime = 2; 

    private float time;                             //current time
    private float spawnTime;

  

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        SetRandomTime();
        time = minTime;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        //Counts up
        time += Time.deltaTime;
 
        // Check to randomly choose table to be occupied
        if (time >= spawnTime)
        {
            int i = Random.Range(0, tables.Length);
            Debug.Log(tables[i]);
            tables[i].GetComponent<Table>().EnableCustomers();
            
            //time = minTime;
        }
        SetRandomTime();

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

}
