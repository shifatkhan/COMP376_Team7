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
        if (cooking)
        {
            timer += Time.deltaTime;
            progressBar.progress = (int)((timer / foodSlot.prepTime) * 100);
        }
    }

    public void QueueFood(FoodSlot food)
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
}
