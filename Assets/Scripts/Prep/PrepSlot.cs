using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepSlot : MonoBehaviour
{
    public bool occupied { get; private set; }

    public FoodSlot foodSlot;
    public GameObject foodGameObject;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void QueueFood(FoodSlot food)
    {
        foodSlot = food;
        occupied = true;
        StartCoroutine(PrepTime());
    }

    IEnumerator PrepTime()
    {
        yield return new WaitForSeconds(foodSlot.prepTime);

        // Food is ready.
        foodGameObject = Instantiate(foodSlot.food.foodPrefab);
        foodGameObject.transform.SetParent(transform);
        foodGameObject.transform.localPosition = Vector3.zero;
        
        foodGameObject.AddComponent<Food>();
        foodGameObject.GetComponent<Food>().SetFood(foodSlot.food);
        foodGameObject.GetComponent<Food>().tableNumber = foodSlot.tableNumber;
    }
}
