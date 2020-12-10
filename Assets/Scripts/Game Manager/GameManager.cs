using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    TableManager tableManager;
    CustomerManager customerManager;
    PuddleManager puddleManager;
    AudioManager audioManager;

    public static float timeLimit = 300;
    public static float currentTime = 0;

    public GameObject WinScreenPrefab;
    public GameObject FailScreenPrefab;

    //Hidden Player Stats for multipliers and end screens. TODO: To be used for later - Nick
    public static int ordersServed = 0;
    public static int puddlesCleaned = 0;
    public static int puddlesSlippedss = 0;
    public static int customersHappy = 0;
    public static int customersAngry = 0;

    // Start is called before the first frame update
    void Start()
    {
        tableManager = TableManager.Instance;
        customerManager = CustomerManager.Instance;
        puddleManager = PuddleManager.Instance;
        audioManager = GetComponentInChildren<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += (Time.deltaTime);

        if(currentTime > timeLimit && ScoreManager.score >= 200)
        {
            StageWin();
        }
        else if(currentTime > timeLimit && ScoreManager.score < 200)
        {
            StageFailure();
        }
    }

    void StageWin()
    {
        DisplayWinScreen();
        Time.timeScale = 0.0f;
    }

    void StageFailure()
    {
        DisplayFailScreen();
        Time.timeScale = 0.0f;
    }

    void DisplayWinScreen()
    {
        WinScreenPrefab.SetActive(true);
    }

    void DisplayFailScreen()
    {
        FailScreenPrefab.SetActive(true);
    }
}
