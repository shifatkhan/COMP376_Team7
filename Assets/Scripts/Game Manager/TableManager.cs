using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a temporary test class that will activate the tables randomly to simulate customers.
/// 
/// @author: ShifatKhan, Nhut Vo
/// </summary>
public class TableManager : MonoBehaviour
{
    public Table[] tables;


    private static TableManager _instance;

    public static TableManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<TableManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(TableManager).Name;
                    _instance = go.AddComponent<TableManager>();
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
        GameObject[] gameTables = GameObject.FindGameObjectsWithTag("Table");

        // Assign a table number to each table.
        for (int i = 0; i < tables.Length; i++)
        {
            tables[i] = gameTables[i].GetComponent<Table>();
            tables[i].tableNumber = i;
            tables[i].UpdateTableNumber();
        }
    }

    public void SetDifficulty(float minOrderTime, float maxOrderTime, int minOrderAmount, int maxOrderAmount, bool waterDepletionEasy, bool waterDepletionMedium)
    {
        // Assign a table number to each table.
        for (int i = 0; i < tables.Length; i++)
        {
            tables[i].minOrderTime = minOrderTime;
            tables[i].maxOrderTime = maxOrderTime;
            tables[i].minOrderAmount = minOrderAmount;
            tables[i].maxOrderAmount = maxOrderAmount;

            if (waterDepletionEasy)
                tables[i].GetComponent<WaterPourable>().drinkSpeedEasy();
            else if (waterDepletionMedium)
                tables[i].GetComponent<WaterPourable>().drinkSpeedMedium();
            else
                tables[i].GetComponent<WaterPourable>().drinkSpeedHard();
        }
    }
}
