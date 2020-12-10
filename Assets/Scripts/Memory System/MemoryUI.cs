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
    private Table tableRef;

    // Left Side
    private Text leftHeader;
    private Text[] leftItems = new Text[4];
    private string leftItemPath = "Left Panel/Item ";

    // Left Side
    private Text rightHeader;
    private Button[] rightSlots = new Button[8];
    private string rightItemPath = "Right Panel/Slot ";

    void Awake()
    {
        // Grab components
        // Left Side
        leftHeader = transform.Find("Left Header Bg/Left Header").GetComponent<Text>();
        leftItems[0] = transform.Find(leftItemPath + "1/Text").GetComponent<Text>();
        leftItems[1] = transform.Find(leftItemPath + "2/Text").GetComponent<Text>();
        leftItems[2] = transform.Find(leftItemPath + "3/Text").GetComponent<Text>();
        leftItems[3] = transform.Find(leftItemPath + "4/Text").GetComponent<Text>();

        // Left Side
        rightHeader = transform.Find("Right Header Bg/Right Header").GetComponent<Text>();
        rightSlots[0] = transform.Find(rightItemPath + "1").GetComponent<Button>();
        rightSlots[1] = transform.Find(rightItemPath + "2").GetComponent<Button>();
        rightSlots[2] = transform.Find(rightItemPath + "3").GetComponent<Button>();
        rightSlots[3] = transform.Find(rightItemPath + "4").GetComponent<Button>();
        rightSlots[4] = transform.Find(rightItemPath + "5").GetComponent<Button>();
        rightSlots[5] = transform.Find(rightItemPath + "6").GetComponent<Button>();
        rightSlots[6] = transform.Find(rightItemPath + "7").GetComponent<Button>();
        rightSlots[7] = transform.Find(rightItemPath + "8").GetComponent<Button>();

        memory.Clear();
        gameObject.SetActive(false);
    }

    public void OpenUIForOrders(Table table, List<FoodSlot> customerOrders)
    {
        PlayerInputManager.enableMovement = false;
        PlayerInputManager.enableThrow = false;
        tableRef = table;

        // Header text
        leftHeader.text = "Memory";
        rightHeader.text = "Customer Orders";

        // Show food that are currently memorized
        UpdateOrdersTakenUI();

        // Show customer's orders
        int numOrders = customerOrders.Count;
        for (int i = 0; i < rightSlots.Length; i++)
        {
            if (i < numOrders)
            {
                // make button selectable for food order
                rightSlots[i].gameObject.SetActive(true);
                rightSlots[i].interactable = true;
                rightSlots[i].animator.SetTrigger("Normal");
                rightSlots[i].GetComponentInChildren<Text>().text = customerOrders[i].foodName;
            }
            else
            {
                // if theres no orders left, leave remaining buttons inactive
                rightSlots[i].gameObject.SetActive(false);
                rightSlots[i].GetComponentInChildren<Text>().text = "";
            }
        }

        gameObject.SetActive(true);
    }

    public void UpdateOrdersTakenUI()
    {
        List<FoodSlot> foodMemorized = memory.GetFoodsMemorized();
        int numMemorized = foodMemorized.Count;
        Debug.LogWarning(numMemorized);
        for (int i = 0; i < leftItems.Length; i++)
        {
            if (i < numMemorized)
                leftItems[i].text = foodMemorized[i].foodName;
            else
                leftItems[i].text = "";
        }
    }

    public void OpenUIForPrep()
    {

    }

    public void CloseUI()
    {
        PlayerInputManager.enableMovement = true;
        PlayerInputManager.enableThrow = true;

        gameObject.SetActive(false);
    }

    public void onSelectOrder(Button button)
    {
        // if memory is full, do nothing
        if (memory.GetFoodsMemorized().Count == 4)
            return;

        button.interactable = false;

        // customers will start waiting once at least one order is taken
        string foodName = button.GetComponentInChildren<Text>().text;
        if (foodName == "") 
            Debug.LogError("BUG: Player clicked on an empty order.");

        tableRef.TakeOrder(foodName);
    }

}
