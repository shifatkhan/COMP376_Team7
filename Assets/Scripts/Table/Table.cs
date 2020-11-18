using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Table class will be keeping track of a table's state and the loop.
/// The loop consists of the TableState.
/// 
/// This script inherits from Interactable, which allows Player to interact on it.
/// 
/// @author: ShifatKhan, Nhut Vo
/// </summary>
public enum TableState
{
    Empty,
    Occupied,
    ReadyToOrder,
    WaitingForFood,
    Eating,
    ReadyToPay
}
public class Table : Interactable
{
    [Header("Table info")]
    public int tableNumber;

    public TableState tableState { get; private set; }

    private FoodSlot order;

    public float pay;
    private GameObject foodOnTable;

    public float maxOrderTime = 20f;
    public float minOrderTime = 5f;

    private FoodFactory foodFactory;

    [Header("Other")]
    [SerializeField]
    private MemoryData memory;

    [SerializeField]
    private GameEvent memoryEvent;

    [SerializeField]
    private Text score;

    public override void Start()
    {
        base.Start();
        transform.Find("Cube").gameObject.SetActive(false);

        tableState = TableState.Empty;

        foodFactory = GameObject.FindGameObjectWithTag("Food Factory").GetComponent<FoodFactory>();

        score = GameObject.Find("Score value").GetComponent<Text>();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnInteract()
    {
        base.OnInteract();

        if (tableState == TableState.ReadyToOrder)
        {
            Waiting();
        }
        else if (tableState == TableState.ReadyToPay)
        {
            Pay();
        }
    }

    public void EnableCustomers()
    {

        //gameObject.transform.GetChild(2).gameObject.SetActive(true);
        transform.Find("Cube").gameObject.SetActive(true);

        tableState = TableState.Occupied;

        StartCoroutine(OrderFood(Random.Range(minOrderTime, maxOrderTime)));
    }

    IEnumerator OrderFood(float orderTime)
    {
        yield return new WaitForSeconds(orderTime);

        // Choose something to order.
        order = new FoodSlot(foodFactory.GetRandomFood(), tableNumber);
        pay = order.price;

        tableState = TableState.ReadyToOrder;

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void Waiting()
    {
        // Add food to memory.
        if (!memory.AddFood(order))
            return;

        memoryEvent.Raise();
        
        tableState = TableState.WaitingForFood;

        order = null;

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void Eating()
    {
        tableState = TableState.Eating;

        StartCoroutine(EatingCo(Random.Range(minOrderTime, maxOrderTime)));

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.cyan;
    }

    IEnumerator EatingCo(float eatingTime)
    {
        yield return new WaitForSeconds(eatingTime);

        // Finished eating.
        Destroy(foodOnTable);

        tableState = TableState.ReadyToPay;

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    public void Pay()
    {
        tableState = TableState.Empty;

        // TODO: Move score to a Game Master gameobject.
        score.text = (int.Parse(score.text) + pay).ToString();

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.Find("Cube").gameObject.SetActive(false);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        // TODO: Fix food pushing player off if it's not the correct food.

        if (other.CompareTag("Food"))
        {
            // Check if a Food was placed on the table.
            Food food = other.GetComponent<Food>();
            if (food == null)
                return;

            // Check if the food placed was ment for this table number.
            if(food.tableNumber == this.tableNumber)
            {
                // Correctly delivered the food.
                other.GetComponent<PickUp>().objectPosition = transform.Find("PickupObject");
                other.GetComponent<PickUp>().PickObjectUp();
                foodOnTable = other.gameObject;

                Eating();
            }
        }
    }
}

