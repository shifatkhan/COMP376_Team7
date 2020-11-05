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
    private List<FoodSlot> foodsMemorized = new List<FoodSlot>();

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
            // GET FOODS
            foodsMemorized = memory.GetFoodsMemorized().ToList<FoodSlot>();
            memory.Clear();

            // PREP FOODS
            for (int i = 0; i < prepSlots.Count; i++)
            {
                prepSlots[i].GetComponent<PrepSlot>().QueueFood(foodsMemorized[i]);
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
