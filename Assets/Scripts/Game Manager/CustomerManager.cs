using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private float spawnTime = 0f;

    [SerializeField]
    public float spawnRate = 2f;

    private TableManager tableManager;

    [SerializeField]
    private GameObject customerPrefab;

    [SerializeField]
    private GameObject spawnPoint;

    private static CustomerManager _instance;

    public static CustomerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CustomerManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(CustomerManager).Name;
                    _instance = go.AddComponent<CustomerManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        //tableManager = GameObject.Find("TableManager").GetComponent<TableManagerJames>();
        tableManager = TableManager.Instance;
    }

    void Update()
    {
        // Check if we should spawn.
        if (Time.time > spawnTime)
        {
            spawnTime += spawnRate;

            // Spawn - ONLY when there are available tables.
            if(AreThereTablesAvailable())
                SpawnCustomers();
        }
    }

    public void SpawnCustomers()
    {
        int tableNumber = Random.Range(0, tableManager.tables.Length);

        //tableManager.tables[tableNumber].chairs.Count;

        while (tableManager.tables[tableNumber].tableState != TableState.Available)
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

    public bool AreThereTablesAvailable()
    {
        bool available = false;

        foreach (Table table in tableManager.tables)
        {
            if (table.tableState == TableState.Available)
            {
                available = true;
                break;
            }
        }

        return available;
    }
}