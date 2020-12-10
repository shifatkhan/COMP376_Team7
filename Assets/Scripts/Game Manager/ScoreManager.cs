using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Keeps track of overall score in a level.
/// It will call ScoreUI to update the UI.
/// 
/// @author ShifatKhan
/// </summary>
public class ScoreManager : MonoBehaviour
{
    static ScoreManager gm;

    public ScoreUI scoreUI; // UI manager for score
    public static ScoreUI ui; // UI manager for score

    [SerializeField] private float goalScore = 10;
    public static float score { get; private set; }
    public static float multiplier { get; private set; }

    void Start()
    {
        multiplier = 1.0f;

        if (scoreUI == null)
            scoreUI = GameObject.Find("Score UI").GetComponent<ScoreUI>();

        ui = scoreUI;
        ui.goalScore = this.goalScore;
    }

    public static void AddScore(float amount)
    {
        if (amount <= 0)
            return;

        // Change score
        score += amount * multiplier;

        // Update UI
        ui.NewScore(score);
    }

    public static void SubstractScore(float amount)
    {
        if (amount <= 0)
            return;

        // Change score
        score -= amount * multiplier;

        // Update UI
        ui.NewScore(score);
    }

    public static void AddMultiplier(float amount)
    {
        if (amount <= 0)
            return;

        multiplier += amount;
    }

    public static void SubstractMultiplier(float amount)
    {
        if (amount <= 0)
            return;

        multiplier -= amount;

    }
}
