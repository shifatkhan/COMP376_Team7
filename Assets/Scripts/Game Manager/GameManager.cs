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

    //
    public float customerSpawnRateMin = 20f;
    public float customerSpawnRateMax = 30f;

    public float maxOrderTime = 20f;
    public float minOrderTime = 5f;

    [SerializeField] private StageCode stageCode;

    [Header("Time")]
    [SerializeField]
    private float timeLimit = 300;
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
    
    // Start is called before the first frame update
    void Start()
    {
        timeLimitStatic = timeLimit;

        tableManager = TableManager.Instance;
        customerManager = CustomerManager.Instance;
        puddleManager = PuddleManager.Instance;
        audioManager = GetComponentInChildren<AudioManager>();
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
        PlayerPrefs.DeleteAll();
        // Change header based on if win or fail
        if (wonStage)
            EndScreenPrefab.Find("Header Text").GetComponent<Text>().text = "Stage Cleared!";
        else
            EndScreenPrefab.Find("Header Text").GetComponent<Text>().text = "Stage Failed.";

        // Set statistics on end screen
        // Avg Tip %
        if (totalTipPercent != 0 && customersPaid != 0)
        EndScreenPrefab.Find("Avg Tip").GetComponent<Text>().text 
            = (totalTipPercent / (float)customersPaid).ToString("F2") + "%";
        // Orders Served
        EndScreenPrefab.Find("Orders Served").GetComponent<Text>().text = ordersServed.ToString();
        // Tips Received
        EndScreenPrefab.Find("Tips Received").GetComponent<Text>().text = ScoreManager.score.ToString("C");

        // store stars & score for this stage
        PlayerPrefs.SetInt(stageCode.ToString(), ScoreManager.CalcStars());
    }

    public void ResumeStage()
    {
        Time.timeScale = 1.0f;
        EndScreenPrefab.gameObject.SetActive(false);
        timeLimitStatic = timeLimit;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadStageSelect()
    {
        SceneManager.LoadScene(1); // Stage Select scene
    }
}
