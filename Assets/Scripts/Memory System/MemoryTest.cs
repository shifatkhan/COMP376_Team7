using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryTest : MonoBehaviour
{
    [SerializeField]
    private MemoryData memory;

    [SerializeField]
    private GameEvent memoryEvent;

    [SerializeField]
    private FoodFactory foodFactory;

    private void Start()
    {
        memory.Clear();
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
