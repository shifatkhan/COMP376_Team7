using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will be representing an instance (gameobject) of a food.
/// This instance will be transfered from the customer to the player to the chef.
/// 
/// @author ShifatKhan
/// </summary>
public class Food : MonoBehaviour
{
    [SerializeField] private FoodData food;
    public string foodName { get; private set; }
    public float price { get; private set; }
    public float prepTime { get; private set; }

    [Tooltip("The table this food is assigned to.")]
    public int tableNumber;

    void Start()
    {
        price = food.price;
        prepTime = food.prepTime;
    }

    public void SetFood(FoodData food)
    {
        this.food = food;
        foodName = food.foodName;
        price = food.price;
        prepTime = food.prepTime;
    }
}
