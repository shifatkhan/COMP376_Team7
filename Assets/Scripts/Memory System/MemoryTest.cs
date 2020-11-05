using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This test class will be placed inside the player later on.
/// This adds the orders into the memory.
/// </summary>
public class MemoryTest : MonoBehaviour
{
    [SerializeField]
    private MemoryData memory;

    [SerializeField]
    private GameEvent memoryEvent;

    [SerializeField]
    private FoodFactory foodFactory;

    public GameObject memoryUI;

    private void Start()
    {
        memory.Clear();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            memoryUI.SetActive(!memoryUI.activeInHierarchy);
        }
    }

    public void AddFoodToMemory()
    {
        memory.AddFood(new FoodSlot(foodFactory.GetRandomFood(), 1));
        memoryEvent.Raise();
    }

    private void OnDestroy()
    {
        memory.Clear();
    }
}
