using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class takes care of the food queue in the prep table.
/// When a food is served, it will start preparing the next food in queue.
/// 
/// @author ShifatKhan
/// </summary>
public class PrepTable : Interactable
{
    [SerializeField]
    private List<Transform> prepSlots;

    [SerializeField]
    private MemoryData memory;

    [SerializeField]
    private Queue<FoodSlot> foodQueue = new Queue<FoodSlot>();

    void Awake()
    {
        prepSlots = new List<Transform>();
    }

    public override void Start()
    {
        base.Start();

        foreach (Transform child in transform)
        {
            prepSlots.Add(child);
        }
    }

    public override void Update()
    {
        base.Update();
        CheckForFreeSlots();
    }

    public override void OnInteract()
    {
        base.OnInteract();

        QueueFoods();
    }

    private void QueueFoods()
    {
        List<FoodSlot> tempFoodsInMemory = memory.GetFoodsMemorized().ToList();

        // GET FOODS
        if (tempFoodsInMemory.Count == 0)
            return;
        else
        {
            print($"Foods prepping: {tempFoodsInMemory.Count}");
            foreach (FoodSlot f in tempFoodsInMemory)
            {
                foodQueue.Enqueue(f);
            }
        }

        memory.Clear();

        // PREP FOODS
        //for (int i = 0; i < prepSlots.Count && foodQueue.Count > 0; i++)
        //{
        //    PrepSlot current = prepSlots[i].GetComponent<PrepSlot>();
        //    if (!current.occupied)
        //    {
        //        current.PrepFood(foodQueue.Dequeue());
        //    }
        //}
    }

    private void CheckForFreeSlots()
    {
        bool free = false;

        // Check if there's a free slot.
        for (int i = 0; i < prepSlots.Count && foodQueue.Count > 0; i++)
        {
            PrepSlot current = prepSlots[i].GetComponent<PrepSlot>();
            if (!current.occupied)
            {
                free = true;
                break;
            }
        }

        // Return void is there are no free slots.
        if (!free)
            return;
        print("Free slot found");
        // PREP FOODS
        for (int i = 0; i < prepSlots.Count && foodQueue.Count > 0; i++)
        {
            PrepSlot current = prepSlots[i].GetComponent<PrepSlot>();
            if (!current.occupied)
            {
                current.PrepFood(foodQueue.Dequeue());
            }
        }
    }
}
