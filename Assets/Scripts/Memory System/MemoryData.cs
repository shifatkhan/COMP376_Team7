using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a list (max 4) of food in memory.
/// </summary>
[CreateAssetMenu(menuName = "Memory")]
public class MemoryData : ScriptableObject
{
    [SerializeField]
    private List<FoodSlot> foodsMemorized = new List<FoodSlot>(); // Maybe make this public?

    public List<FoodSlot> GetFoodsMemorized()
    {
        return foodsMemorized;
    }

    public bool AddFood(FoodSlot food)
    {
        if (foodsMemorized.Count >= 4)
        {
            Debug.Log("FoodsMemorized is full");
            return false;
        }
            

        foodsMemorized.Add(food);
        return true;
    }

    public bool RemoveItem(FoodSlot food)
    {
        if (foodsMemorized.Count <= 0)
            return false;

        foreach (FoodSlot f in foodsMemorized)
        {
            if (f == food)
            {
                foodsMemorized.Remove(f);
                return true;
            }
        }

        return false;
    }

    public void Clear()
    {
        foodsMemorized.Clear();
    }
}

/// <summary>
/// A serializable version of Food. This is used because the normal Food.cs is 
/// a mono behaviour, and therefore cannot be added to the MemoryData.
/// </summary>
[System.Serializable]
public class FoodSlot
{
    public FoodData food;
    public string foodName;
    public float price;
    public float prepTime;
    public int tableNumber;

    public FoodSlot(FoodData food, int tableNumber)
    {
        this.food = food;
        this.foodName = food.foodName;
        this.price = food.price;
        this.prepTime = food.prepTime;
        this.tableNumber = tableNumber;
    }

}
