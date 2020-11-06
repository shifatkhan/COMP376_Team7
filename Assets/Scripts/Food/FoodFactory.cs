using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will be attached to the Game Manager and will be accessible by all customers.
/// This class will take care of holding all possible foods in the current level.
/// It's a factory that will return a particular food.
/// 
/// @author ShifatKhan
/// </summary>
public class FoodFactory : MonoBehaviour
{
    public FoodData[] foods;

    public FoodData GetRandomFood()
    {
        int index = Random.Range(0, foods.Length);
        return foods[index];
    }
}
