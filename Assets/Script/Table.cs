using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public int tableNumber;
    public bool isOccupied;                    //If the table already has a group on it

    public FoodSlot order;
    public bool readyToOrder; // TODO: Change these booleans into an Enum state.
    public bool waiting;
    public bool eating;
    public bool readyToPay;

    public float pay;
    private GameObject foodOnTable;

    public float maxOrderTime = 20f;
    public float minOrderTime = 5f;

    private FoodFactory foodFactory;

    [SerializeField]
    private bool canInteract = false;

    [SerializeField]
    private MemoryData memory;

    [SerializeField]
    private GameEvent memoryEvent;

    [SerializeField]
    private Text score;

    public void Start()
    {
        transform.Find("Cube").gameObject.SetActive(false);
        readyToOrder = false;
        isOccupied = false;
        foodFactory = GameObject.Find("FoodFactory - Shifat").GetComponent<FoodFactory>();

        score = GameObject.Find("Score value").GetComponent<Text>();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        //gameObject.transform.GetChild(2).gameObject.SetActive(false);
        if(Input.GetButtonDown("Fire1") && canInteract)
        {
            if (readyToOrder)
            {
                Waiting();
            }
            else if (readyToPay)
            {
                Pay();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Make table so it occupied
    // As for the prototype, it just changed the color to red --> means O-C-C-U-P-I-E-D
    public void EnableCustomers()
    {

        //gameObject.transform.GetChild(2).gameObject.SetActive(true);
        transform.Find("Cube").gameObject.SetActive(true);
        isOccupied = true;
        StartCoroutine(OrderFood(Random.Range(minOrderTime, maxOrderTime)));
    }

    IEnumerator OrderFood(float orderTime)
    {
        yield return new WaitForSeconds(orderTime);

        // Choose something to order.
        order = new FoodSlot(foodFactory.GetRandomFood(), tableNumber);
        pay = order.price;

        readyToOrder = true;

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void Waiting()
    {
        // Add food to memory.
        if (!memory.AddFood(order))
            return;
        memoryEvent.Raise();
        readyToOrder = false;
        waiting = true;
        order = null;

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void Eating()
    {
        waiting = false;
        eating = true;

        StartCoroutine(EatingCo(Random.Range(minOrderTime, maxOrderTime)));

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.cyan;
    }

    IEnumerator EatingCo(float eatingTime)
    {
        yield return new WaitForSeconds(eatingTime);

        // Finished eating.
        Destroy(foodOnTable);
        
        eating = false;
        readyToPay = true;

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    public void Pay()
    {
        isOccupied = false;
        readyToPay = false;

        // TODO: Move score to a Game Master gameobject.
        score.text = (int.Parse(score.text) + pay).ToString();

        transform.Find("Cube").gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.Find("Cube").gameObject.SetActive(false);
    }

    public void Seated()
    {
        transform.Find("Cube").gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
        else if (other.CompareTag("Mop")) // Mop is a temporary tag. It represents objects that can be picked up.
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

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}

