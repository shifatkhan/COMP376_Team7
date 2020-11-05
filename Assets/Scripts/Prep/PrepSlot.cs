using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepSlot : MonoBehaviour
{
    public bool occupied { get; private set; }

    public FoodSlot foodSlot;
    public GameObject foodGameObject;

    private RadialProgressBar progressBar;
    private GameObject progressBarGameObject;

    private float timer;
    private bool cooking = false;

    private bool canInteract = false; // Checks if player is in range.

    private void Awake()
    {
        progressBar = GetComponentInChildren<RadialProgressBar>();
        progressBarGameObject = progressBar.gameObject;
    }

    void Start()
    {
        occupied = false;
        progressBarGameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && canInteract)
        {
            TakeFood();
        }

        if (cooking)
        {
            timer += Time.deltaTime;
            progressBar.progress = (int)((timer / foodSlot.prepTime) * 100);
        }
    }

    public void PrepFood(FoodSlot food)
    {
        foodSlot = food;
        
        StartCoroutine(PrepTime());
    }

    IEnumerator PrepTime()
    {
        occupied = true;
        progressBarGameObject.SetActive(true);

        cooking = true;

        yield return new WaitForSeconds(foodSlot.prepTime);

        cooking = false;
        timer = 0;
        progressBarGameObject.SetActive(false);

        // Food is ready.
        foodGameObject = Instantiate(foodSlot.food.foodPrefab);
        foodGameObject.transform.SetParent(transform);
        foodGameObject.transform.localPosition = Vector3.zero;
        
        foodGameObject.AddComponent<Food>();
        foodGameObject.GetComponent<Food>().SetFood(foodSlot.food);
        foodGameObject.GetComponent<Food>().tableNumber = foodSlot.tableNumber;
    }

    public void TakeFood()
    {
        // Don't do anything if it is still cooking.
        if (cooking)
            return;

        ResetSlot();
    }

    private void ResetSlot()
    {
        occupied = false;
        cooking = false;
        timer = 0;
        progressBarGameObject.SetActive(false);
        foodGameObject = null;
        foodSlot = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
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
