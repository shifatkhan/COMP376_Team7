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

    public bool canInteract = false;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3") && canInteract)
        {
            QueueFoods();
        }
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
        print($"foodsMemorized: {foodQueue.Count}");

        // PREP FOODS
        //for (int i = 0, j = 0; i < prepSlots.Count && j < foodsMemorized.Count; i++,j++)
        //{
        //    print($"foodsMemorized{i}: {foodsMemorized.Count}");
        //    PrepSlot current = prepSlots[i].GetComponent<PrepSlot>();
        //    if (!current.occupied)
        //    {
        //        current.QueueFood(foodsMemorized[i]);
        //        foodsMemorized.RemoveAt(i);
        //        j--; // Since we removed an item.
        //    }
        //}

        for (int i = 0; i < prepSlots.Count && foodQueue.Count > 0; i++)
        {
            PrepSlot current = prepSlots[i].GetComponent<PrepSlot>();
            if (!current.occupied)
            {
                current.QueueFood(foodQueue.Dequeue());
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
