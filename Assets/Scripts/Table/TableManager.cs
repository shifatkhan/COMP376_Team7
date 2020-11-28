using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a temporary test class that will activate the tables randomly to simulate customers.
/// 
/// @author: ShifatKhan, Nhut Vo
/// </summary>
/// // TODO: Delete this file.
[System.Obsolete("This class is getting replaced by TableManagerJames.cs (use GameManager-James gameobject)", true)]
public class TableManager : MonoBehaviour
{
    public Table[] tables;

    private float spawnTime = 0f;

    [SerializeField]
    private float spawnRate = 2f;

    void Start()
    {
        spawnTime = Time.time + spawnRate;

        // Assign a table number to each table.
        for (int i = 0; i < tables.Length; i++)
        {
            tables[i].tableNumber = i;
        }
    }

    void Update()
    {

        //// Check if we should spawn.
        //if (Time.time > spawnTime)
        //{
        //    spawnTime += spawnRate;

        //    int i = Random.Range(0, tables.Length);
        //    // Check if table is occupied
        //    if (tables[i].tableState == TableState.Available)
        //    {
        //        tables[i].EnableCustomers();
        //    }
        //}
    }
}


