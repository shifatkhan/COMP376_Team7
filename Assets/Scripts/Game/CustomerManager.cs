using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private float spawnTime = 0f;

    [SerializeField]
    private float spawnRate = 2f;

    private TableManager tableManager;

    [SerializeField]
    private GameObject customerPrefab;

    [SerializeField]
    private GameObject spawnPoint;

    void Start()
    {
        tableManager = GameObject.Find("TableManager").GetComponent<TableManager>();
    }

    void Update()
    {
        // Check if we should spawn.
        if (Time.time > spawnTime)
        {
            spawnTime += spawnRate;

            // Spawn
            SpawnCustomers();
        }
    }

    // TODO: Don't run this if all tables are occupied.
    public void SpawnCustomers()
    {
        int tableNumber = Random.Range(0, tableManager.tables.Length);

        //tableManager.tables[tableNumber].chairs.Count;

        while(tableManager.tables[tableNumber].tableState != TableState.Available)
        {
            tableNumber = Random.Range(0, tableManager.tables.Length);
        }

        int customersToSpawn = tableManager.tables[tableNumber].chairs.Count;

        for (int i = 0; i < customersToSpawn; i++)
        {
            GameObject customer = Instantiate(customerPrefab, spawnPoint.transform.position, Quaternion.identity);
            customer.GetComponent<NpcMoveToTable>().SetTableNumber(tableNumber);
        }
    }
}
