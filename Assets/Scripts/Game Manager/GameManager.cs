using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    TableManager tableManager = TableManager.Instance;
    CustomerManager customerManager = CustomerManager.Instance;
    PuddleManager puddleManager = PuddleManager.Instance;
    AudioManager audioManager;
    ScoreManager scoreManager;

    public static float timeLimit = 300;
    public static float currentTime = 0;

    //Hidden Player Stats for multipliers and end screens. TODO: To be used for later - Nick
    private int ordersServed = 0;
    private int puddlesCleaned = 0;
    private int puddlesSlippedss = 0;
    private int customersHappy = 0;
    private int customersAngry = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GetComponentInChildren<AudioManager>();
        scoreManager = GetComponentInChildren<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.deltaTime;

        if(ScoreManager.score == 200 && currentTime < timeLimit)
        {
            StageSuccess();
        }
        else if(currentTime > timeLimit && ScoreManager.score < 200)
        {
            StageFailure();
        }
    }

    void StageSuccess()
    {

    }

    void StageFailure()
    {

    }
}
