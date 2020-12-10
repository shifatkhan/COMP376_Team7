using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

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
    //****************** TABLE INFO ******************//
    [Header("Table info")]
    public int tableNumber;
    public List<GameObject> chairs { get; private set; }
    public bool[] occupiedChairs { get; private set; }

    private GameObject foodOnTable;

    //****************** SCORING ******************//
    [Header("Order")]
    [Range(0,1)]
    [SerializeField] private float baseTip = 0.15f; // Tip to add to totalPay
    [Min(1)]
    [SerializeField] private float bonusMultiplier =  1.2f; // Bonus to multiply on baseTip
    [SerializeField] private float totalPay = 0; // Total amount to be paid
    
    //****************** ORDERING ******************//
    public TableState tableState { get; private set; }
    public float maxOrderTime = 20f;
    public float minOrderTime = 5f;

    //****************** MEMORY ******************//
    [Header("Other")]
    [SerializeField]
    private MemoryData memory;
    [SerializeField]
    private GameEvent memoryEvent;

    private FoodFactory foodFactory;
    private List<FoodSlot> allOrders;
    private List<FoodSlot> currOrders = new List<FoodSlot>();

    //****************** UI ******************//
    [SerializeField] private Transform T_memoryUI;
    private MemoryUI memoryUI;
    private PatienceMeter patienceManager;
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
        patienceManager = GetComponent<PatienceMeter>();
        waterManager = GetComponent<WaterPourable>();
        tableStateAnim = transform.Find("Table State UI/Bubble/Table State").GetComponent<Animator>();
        memoryUI = T_memoryUI.GetComponent<MemoryUI>();

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
            // customer's order is taken
            memoryUI.OpenUIForOrders(this, allOrders);
            // note Waiting() now called at bottom when player takes an order
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
        patienceManager.setActive(true);
        patienceManager.resetPatience();
        waterManager.setActive(true);
        waterManager.waterFilled();
        // TODO adjust difficulty by calling one of waterManager's method

        updateStateInUI();
        StopAllCoroutines(); // This fixes the glitch where a table places multiple orders.
        StartCoroutine(OrderFood(Random.Range(minOrderTime, maxOrderTime)));
    }

    IEnumerator OrderFood(float orderTime)
    {
        yield return new WaitForSeconds(orderTime);
        tableState = TableState.ReadyToOrder;

        // Choose something to order.
        int numOfFoodOrders = Random.Range(1, 9); // 1 to 8
        allOrders = new List<FoodSlot>(numOfFoodOrders);
        for (int i = 0; i < numOfFoodOrders; i++)
        {
            allOrders.Add(new FoodSlot(foodFactory.GetRandomFood(), tableNumber));
            totalPay += allOrders[i].price;
        }

        updateStateInUI();
    }

    public void Waiting()
    {
        tableState = TableState.WaitingForFood;
        memoryEvent.Raise();

        updateStateInUI();
    }

    public void Eating()
    {
        tableState = TableState.Eating;
        patienceManager.setActive(false); // stop depleting patience when eating

        StartCoroutine(EatingCo(Random.Range(minOrderTime, maxOrderTime)));

        updateStateInUI();
    }

    IEnumerator EatingCo(float eatingTime)
    {
        yield return new WaitForSeconds(eatingTime);

        // Finished eating. If all orders are met, they pay
        // else, they will want to order more
        if (allOrders.Count == 0)
        {
            tableState = TableState.ReadyToPay;
            Destroy(foodOnTable);
        }
        else
            tableState = TableState.ReadyToOrder;

        patienceManager.setActive(true); // start depleting patience again

        updateStateInUI();
    }

    public void Pay()
    {
        tableState = TableState.Available;
		
        patienceManager.setActive(false);
        waterManager.setActive(false);

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
        totalPay = totalTip * totalPay;

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
            if (food == null || currOrders.Count == 0)
                return;

            // Check if the food placed was meant for this table number.
            foreach (FoodSlot order in currOrders)
            {
                if (food.foodName == order.foodName)
                {
                    // Correctly delivered the food.
                    other.GetComponent<PickUp>().objectPosition = transform.Find("PickupObject");
                    other.GetComponent<PickUp>().PickObjectUp();
                    foodOnTable = other.gameObject;

                    patienceManager.increPatience(0.25f);

                    // only when they receive all their current orders will they start eating
                    // else, they will keep waiting
                    if (currOrders.Count == 0)
                        Eating();
                }
            }
            
        }
        else if (other.CompareTag("Customer"))
        {
            if (other.GetComponent<NpcMoveToTable>().tableNumber == this.tableNumber)
            {
                this.EnableCustomers();
                for (int i = 0; i < occupiedChairs.Length; i++)
                {
                    if (!occupiedChairs[i])
                    {
                        other.GetComponent<NpcMoveToTable>().EnableSittingAnimation();
                        other.GetComponent<NpcMoveToTable>().DisableAIMovement();
                        other.GetComponent<Collider>().enabled = false;
                        Destroy(other.GetComponent<Rigidbody>());
                        other.transform.position = chairs[i].transform.position;
                        other.transform.LookAt(transform);
                        other.transform.Translate(-0.5f, 0.5f, 0.0f);
                        occupiedChairs[i] = true;
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
    }

    public void TakeOrder(string foodName)
    {
        for (int i = 0; i < allOrders.Count; i++)
        {
            if (allOrders[i].foodName == foodName)
            {
                // add taken order to memory and remove it from customer's orders
                memory.AddFood(allOrders[i]);
                currOrders.Add(allOrders[i]);
                allOrders.RemoveAt(i);

                break; // break in case theres a duplicate order
            }
        }

        Waiting();
        memoryUI.UpdateOrdersTakenUI();
    }
}

