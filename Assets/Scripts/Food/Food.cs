using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will be representing an instance (or gameobject) of a food.
/// This instance will be transfered from the customer to the player to the chef.
/// 
/// @author ShifatKhan
/// </summary>
public class Food : MonoBehaviour
{
    [SerializeField] private FoodData food;
    public float cost { get; private set; }
    public float prepTime { get; private set; }

    void Start()
    {
        cost = food.cost;
        prepTime = food.prepTime;
    }

    public void SetFood(FoodData food)
    {
        this.food = food;
        cost = food.cost;
        prepTime = food.prepTime;
    }
}
