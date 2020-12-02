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
    Available,
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

    public List<GameObject> chairs { get; private set; }
    public bool[] occupiedChairs { get; private set; }

    [Header("Other")]
    [SerializeField]
    private MemoryData memory;

    [SerializeField]
    private GameEvent memoryEvent;

    //*** UI ***//
    private WaterPourable waterManager;
    private Animator tableStateAnim;

    public override void Start()
    {
        base.Start();

        chairs = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "Chair")
            {
                chairs.Add(child.gameObject);
            }
        }

        occupiedChairs = new bool[chairs.Count];
        tableState = TableState.Available;

        // find components
        transform.Find("Cube").gameObject.SetActive(false);
        foodFactory = GameObject.FindGameObjectWithTag("Food Factory").GetComponent<FoodFactory>();
        waterManager = GetComponent<WaterPourable>();
        tableStateAnim = transform.Find("Table State UI/Bubble/Table State").GetComponent<Animator>();

        this.updateStateInUI();
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
        transform.Find("Cube").gameObject.SetActive(true);

        tableState = TableState.Occupied;

        // customers start drinking water
        waterManager.startDrinking();
        // TODO adjust difficulty by calling one of waterManager's method

        updateStateInUI();
        StopAllCoroutines(); // This fixes the glitch where a table places multiple orders.
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

        updateStateInUI();
    }

    public void Waiting()
    {
        // Add food to memory.
        if (!memory.AddFood(order))
            return;

        memoryEvent.Raise();
        
        tableState = TableState.WaitingForFood;

        //order = null;

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        updateStateInUI();
    }

    public void Eating()
    {
        tableState = TableState.Eating;

        StartCoroutine(EatingCo(Random.Range(minOrderTime, maxOrderTime)));

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.cyan;

        updateStateInUI();
    }

    IEnumerator EatingCo(float eatingTime)
    {
        yield return new WaitForSeconds(eatingTime);

        // Finished eating.
        Destroy(foodOnTable);

        tableState = TableState.ReadyToPay;

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.white;

        updateStateInUI();
    }

    public void Pay()
    {
        tableState = TableState.Available;

        // TODO: Move score to a Game Master gameobject, which will update ScoreUI gameobject reference
        //score.text = (int.Parse(score.text) + pay).ToString();

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.Find("Cube").gameObject.SetActive(false);

        order = null;

        updateStateInUI();
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
            if(food.foodName == this.order.foodName)
            {
                // Correctly delivered the food.
                other.GetComponent<PickUp>().objectPosition = transform.Find("PickupObject");
                other.GetComponent<PickUp>().PickObjectUp();
                foodOnTable = other.gameObject;

                Eating();
            }
        }
        else if (other.CompareTag("Customer"))
        {
            if (other.GetComponent<NpcMoveToTable>().tableNumber == this.tableNumber)
            {
                for (int i = 0; i < occupiedChairs.Length; i++)
                {
                    if (!occupiedChairs[i])
                    {
                        other.GetComponent<NpcMoveToTable>().DisableAIMovement();
                        other.GetComponent<BoxCollider>().enabled = false;
                        other.transform.position = chairs[i].transform.position;
                        occupiedChairs[i] = true;
                        this.EnableCustomers();
                        break;
                    }
                }
            }
        }
    }

    public void UpdateTableNumber()
    {
        // assign table # in its UI and the state
        transform.Find("Table State UI/Bubble/Table Number").GetComponent<Text>().text = (tableNumber + 1).ToString();
    }

    private void updateStateInUI()
    {
        tableStateAnim.SetBool("ReadyToOrder", false);
        tableStateAnim.SetBool("AwaitingFood", false);
        tableStateAnim.SetBool("ReadyToPay", false);

        switch (this.tableState)
        {
            case TableState.ReadyToOrder:
                tableStateAnim.SetBool("ReadyToOrder", true); break;
            case TableState.WaitingForFood:
                tableStateAnim.SetBool("AwaitingFood", true); break;
            case TableState.ReadyToPay:
                tableStateAnim.SetBool("ReadyToPay", true); break;
            default:
                break;
        }

        // TODO same for patience
    }
}

