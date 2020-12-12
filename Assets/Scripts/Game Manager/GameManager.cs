using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the state and configuration of 
/// the stage. Has references to other managers.
/// 
/// @author Thanh Tung Nguyen
/// </summary>
public class GameManager : MonoBehaviour
{
    TableManager tableManager;
    CustomerManager customerManager;
    PuddleManager puddleManager;
    AudioManager audioManager;
    ScoreManager scoreManager;

    [SerializeField] private StageCode stageCode;

    [Header ("Difficulty: Customers")]
    //****************** CUSTOMER ******************//
    public float customerSpawnRateMin = 20f;
    public float customerSpawnRateMax = 30f;

    [Header("Difficulty: Table")]
    //****************** TABLE ******************//
    [Min(0)]
    public float minOrderTime = 5f;
    [Min(0)]
    public float maxOrderTime = 20f;
    [Min(1)]
    public int minOrderAmount = 1;
    [Min(1)]
    public int maxOrderAmount = 9; 

    [Header("Difficulty: Water")]
    //****************** WATER ******************//
    public bool waterDepletionEasy = true;
    public bool waterDepletionMedium = false;
    public bool waterDepletionHard = false;

    [Header("Difficulty: Skill Check")]
    //*************** SKILL CHECK ***************//
    [Range(0.3f, 1.0f)]
    public float pouringSpeedRate = 0.5f;
    [Range(0.05f, 0.1f)]
    public float perfectZoneSize = 0.05f;

    [Header("Difficulty: Puddle")]
    //****************** PUDDLE ******************//
    [Min(0)]
    public float puddleMinSpawnRate = 20f;
    [Min(0)]
    public float puddleMaxSpawnRate = 30f;

    [Header("Difficulty: Score")]
    //****************** TIME ******************//
    [SerializeField]
    [Min(1)]
    private float timeLimit = 300;
    public float goalScore = 10;
    public float twoStarsGoal;
    public float threeStarsGoal;
    public static float timeLimitStatic = 300;
    public static float currentTimeStatic = 0;

    public Transform EndScreenPrefab;

    [Header("Stats")]
    //Hidden Player Stats for multipliers and end screens. TODO: To be used for later - Nick
    public static int ordersServed = 0;
    public static int puddlesCleaned = 0;
    public static int puddlesSlippedss = 0;
    public static int customersHappy = 0;
    public static int customersAngry = 0;
    // to calc average tip %
    public static float totalTipPercent = 0;
    public static int customersPaid = 0;

    private void Awake()
    {
        currentTimeStatic = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLimitStatic = timeLimit;

        tableManager = TableManager.Instance;
        customerManager = CustomerManager.Instance;
        puddleManager = PuddleManager.Instance;
        audioManager = GetComponentInChildren<AudioManager>();
        scoreManager = GetComponentInChildren<ScoreManager>();

        tableManager.SetDifficulty(minOrderTime, maxOrderTime, minOrderAmount, maxOrderAmount, waterDepletionEasy, waterDepletionMedium, pouringSpeedRate, perfectZoneSize);
        customerManager.spawnRateMin = customerSpawnRateMin;
        customerManager.spawnRateMax = customerSpawnRateMax;
        puddleManager.minSpawnTime = puddleMinSpawnRate;
        puddleManager.maxSpawnTime = puddleMaxSpawnRate;
        ScoreManager.goalScoreStatic = goalScore;
        scoreManager.twoStarsGoal = twoStarsGoal;
        scoreManager.threeStarsGoal = threeStarsGoal; 
        GameObject.Find("Score UI").GetComponent<ScoreUI>().goalScore = goalScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeStatic += (Time.deltaTime);

        // determine if player won or lost respectively
        if(currentTimeStatic > timeLimitStatic && ScoreManager.score >= ScoreManager.goalScoreStatic)
        {
            ShowEndScreen(true);
        }
        else if(currentTimeStatic > timeLimitStatic && ScoreManager.score < ScoreManager.goalScoreStatic)
        {
            ShowEndScreen(false);
        }
    }

    private void ShowEndScreen(bool wonStage)
    {
        EndScreenPrefab.gameObject.SetActive(true);
        Time.timeScale = 0.0f;

        // Change header based on if win or fail
        if (wonStage)
            EndScreenPrefab.Find("Header Text").GetComponent<Text>().text = "Stage Cleared!";
        else
            EndScreenPrefab.Find("Header Text").GetComponent<Text>().text = "Stage Failed.";

        // Show statistics on EndScreen
        CalcStatistics();

        // store stars & score for this stage
        PlayerPrefs.SetInt(stageCode.ToString(), ScoreManager.CalcStars());

        // Show Buttons appropriately
        EndScreenPrefab.Find("Resume Btn").GetComponent<Button>().gameObject.SetActive(false);
    }

    public void CalcStatistics()
    {
        // Show statistics on EndScreen
        // Avg Tip %
        if (totalTipPercent != 0 && customersPaid != 0)
            EndScreenPrefab.Find("Avg Tip").GetComponent<Text>().text
                = ((totalTipPercent / customersPaid)*100).ToString("F1") + "%";
        // Orders Served
        EndScreenPrefab.Find("Orders Served").GetComponent<Text>().text = ordersServed.ToString();
        // Tips Received
        EndScreenPrefab.Find("Tips Received").GetComponent<Text>().text = ScoreManager.score.ToString("C");
    }

}
