using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a ScriptableObject that holds data/stats about a particular food.
/// In other words, this class will not represent any particular instance of
/// a food. If you want an instance, look at Food.cs
/// 
/// @author ShifatKhan
/// </summary>
[CreateAssetMenu(menuName = "Food")]
public class FoodData : ScriptableObject
{
    public string foodName;
    public float price;
    public float prepTime;
    public GameObject foodPrefab;
}
