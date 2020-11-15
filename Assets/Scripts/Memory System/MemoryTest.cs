using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This test class will be placed inside the player later on.
/// This adds the orders into the memory.
/// 
/// @author ShifatKhan
/// </summary>
public class MemoryTest : MonoBehaviour
{
    [SerializeField]
    private MemoryData memory;

    [SerializeField]
    private GameEvent memoryEvent;

    private FoodFactory foodFactory;

    public GameObject memoryUI;

    private void Start()
    {
        memory.Clear();
        //foodFactory = GameObject.Find("FoodFactory - Shifat").GetComponent<FoodFactory>();
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
