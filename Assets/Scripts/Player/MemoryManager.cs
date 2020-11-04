using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a list (max 4) of food in memory.
/// 
/// @author ShifatKhan
/// </summary>
public class MemoryManager : MonoBehaviour
{
    private List<Food> foodMemorized;

    void Start()
    {
        foodMemorized = new List<Food>(4);
    }

    void Update()
    {
        
    }

    public bool AddFood(Food food)
    {
        if (foodMemorized.Count >= 4)
            return false;

        foodMemorized.Add(food);
        return true;
    }
}
