using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Keeps track of overall score in a level.
/// It will call ScoreUI to update the UI.
/// 
/// @author ShifatKhan, Thanh Tung Nguyen
/// </summary>
public class ScoreManager : MonoBehaviour
{
    static ScoreManager instance;

    public ScoreUI scoreUI; // UI manager for score
    public static ScoreUI ui; // UI manager for score

    public float twoStarsGoal;
    public float threeStarsGoal;
    public static float goalScoreStatic;
    public static float twoStarsGoalStatic;
    public static float threeStarsGoalStatic;
    public static float score { get; private set; }

    void Start()
    {
        if (scoreUI == null)
            scoreUI = GameObject.Find("Score UI").GetComponent<ScoreUI>();

        ui = scoreUI;
        //ui.goalScore = goalScoreStatic;

        twoStarsGoalStatic = twoStarsGoal;
        threeStarsGoalStatic = threeStarsGoal;
    }

    public static void AddScore(float amount)
    {
        if (amount <= 0)
            return;

        // Round to 2 decimal points
        amount = Mathf.Round(amount * 100f) / 100f;

        // Change score
        score += amount;

        // Update UI
        ui.NewScore(score);
    }

    public static void SubstractScore(float amount)
    {
        if (amount <= 0)
            return;

        // Round to 2 decimal points
        amount = Mathf.Round(amount * 100f) / 100f;

        // Change score
        score -= amount;

        // Update UI
        ui.NewScore(score);
    }

    public static int CalcStars()
    {
        if (score >= threeStarsGoalStatic)
            return 3;
        else if (score >= twoStarsGoalStatic)
            return 2;
        else if (score >= goalScoreStatic)
            return 1;
        else
            return 0;
    }
}
