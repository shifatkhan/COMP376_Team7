using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    TableManager tableManager;
    CustomerManager customerManager;
    PuddleManager puddleManager;
    AudioManager audioManager;

    [SerializeField]
    private float timeLimit = 300;
    public static float timeLimitStatic = 300;
    public static float currentTimeStatic = 0;

    public GameObject WinScreenPrefab;
    public GameObject FailScreenPrefab;

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

        if(currentTimeStatic > timeLimitStatic && ScoreManager.score >= ScoreManager.goalScoreStatic)
        {
            StageWin();
        }
        else if(currentTimeStatic > timeLimitStatic && ScoreManager.score < ScoreManager.goalScoreStatic)
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
