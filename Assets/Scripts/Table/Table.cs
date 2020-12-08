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

    [Header("Order")]
    [Range(0,1)]
    [SerializeField] private float baseTip = 0.15f; // Tip to add to totalPay
    [Min(1)]
    [SerializeField] private float bonusMultiplier =  1.2f; // Bonus to multiply on baseTip
    [SerializeField] private float totalPay; // Total amount to be paid
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
    private Text stateUIText;

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

        // find scripts
        transform.Find("Cube").gameObject.SetActive(false);
        foodFactory = GameObject.FindGameObjectWithTag("Food Factory").GetComponent<FoodFactory>();
        waterManager = GetComponent<WaterPourable>();
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
        totalPay = order.price;

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

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.Find("Cube").gameObject.SetActive(false);

        order = null;

        CalculateTotalPay();
        updateStateInUI();
    }

    public void AddBaseTip(float amount)
    {
        // Base tip can't go above 1.
        if (amount <= 0 || amount + baseTip > 1)
            return;

        baseTip += amount;
    }

    public void SubstractBaseTip(float amount)
    {
        // Base tip can't go below 0.
        if (amount <= 0 || baseTip - amount < 0)
            return;

        baseTip -= amount;
    }

    public void AddBonusMultiplier(float amount)
    {
        if (amount <= 0)
            return;

        bonusMultiplier += amount;
    }

    public void SubstractBonusMultiplier(float amount)
    {
        // Base tip can't go below 1.
        if (amount <= 0 || bonusMultiplier - amount < 1)
            return;

        bonusMultiplier -= amount;
    }

    /// <summary>
    /// This should be called at the end.
    /// AKA when the table is going to pay "Pay()"
    /// 
    /// We also update the score in Score Manager.
    /// </summary>
    /// <returns>totalPay: the final pay</returns>
    public float CalculateTotalPay()
    {
        // Calculate total tip
        float totalTip = baseTip + (baseTip * bonusMultiplier);

        // Calculate total pay
        totalPay += totalTip;

        // Update score
        ScoreManager.AddScore(totalPay);

        return totalPay;
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

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
                        other.GetComponent<NpcMoveToTable>().EnableSitAnimation();
                        other.GetComponent<NpcMoveToTable>().DisableAIMovement();
                        other.GetComponent<Collider>().enabled = false;
                        Destroy(other.GetComponent<Rigidbody>());
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
        transform.Find("Water Status Position/Water Status Canvas/Bubble/Table Number Text")
            .GetComponent<Text>().text = (tableNumber + 1).ToString();
        stateUIText = transform.Find("Water Status Position/Water Status Canvas/Bubble/Table State")
            .GetComponent<Text>();
        this.updateStateInUI();
    }

    private void updateStateInUI()
    {
        switch (this.tableState)
        {
            case TableState.Available:
                stateUIText.text = "Vacant."; break;
            case TableState.Occupied:
                stateUIText.text = "Deciding.."; break;
            case TableState.ReadyToOrder:
                stateUIText.text = "Ready to order!"; break;
            case TableState.WaitingForFood:
                stateUIText.text = "Awaiting food.."; break;
            case TableState.Eating:
                stateUIText.text = "Dining."; break;
            case TableState.ReadyToPay:
                stateUIText.text = "Ready to pay!"; break;
            default:
                break;
        }
    }
}

