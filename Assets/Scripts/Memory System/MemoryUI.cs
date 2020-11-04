using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds a list (max 4) of food in memory.
/// 
/// @author ShifatKhan
/// </summary>
public class MemoryUI : MonoBehaviour
{
    [SerializeField]
    private MemoryData memory;

    [SerializeField]
    private GameObject memorySlot;

    /// <summary>
    /// Refresh the memory slots found inside the memory UI.
    /// This will look into the MemoryData scriptable object to 
    /// repopulate the slots.
    /// </summary>
    public void UpdateMemoryUI()
    {
        // Remove old items before rebuilding memory.
        // This avoids duplicate slots.
        foreach (Transform child in transform)
        {
            if (child == memorySlot) continue;
            Destroy(child.gameObject);
        }

        // Create a new slot for each food in the memory.
        List<FoodSlot> foodsMemorized = memory.GetFoodsMemorized();
        foreach (FoodSlot food in foodsMemorized)
        {
            GameObject newSlot = Instantiate(memorySlot);

            newSlot.transform.Find("Food Name text").GetComponent<Text>().text = food.foodName;
            Text t = newSlot.transform.Find("Food Price text").Find("Food Price value").GetComponent<Text>();
            t.text = food.price.ToString();
            newSlot.transform.Find("Food Prep text").Find("Food Prep value").GetComponent<Text>().text = food.prepTime.ToString();

            newSlot.transform.SetParent(transform);
        }
    }
}
