using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrepTable : MonoBehaviour
{
    [SerializeField]
    private List<Transform> prepSlots;

    [SerializeField]
    private MemoryData memory;

    [SerializeField]
    private Queue<FoodSlot> foodQueue = new Queue<FoodSlot>();

    [SerializeField]
    private bool canInteract = false; // Checks if player is in range.

    void Awake()
    {
        prepSlots = new List<Transform>();
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            prepSlots.Add(child);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3") && canInteract)
        {
            QueueFoods();
        }

        CheckForFreeSlots();
    }

    private void QueueFoods()
    {
        List<FoodSlot> tempFoodsInMemory = memory.GetFoodsMemorized().ToList();

        // GET FOODS
        if (tempFoodsInMemory.Count == 0)
            return;
        else
        {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
