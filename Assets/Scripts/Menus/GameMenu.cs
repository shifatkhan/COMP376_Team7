using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    //*** UI ***//
    private Image starImg1;
    private Image starImg2;
    private Image starImg3;

    private Text starTxt1;
    private Text starTxt2;
    private Text starTxt3;

    private Text totalTips;

    void Awake()
    {
        // get components
        starImg1 = transform.Find("star 1").GetComponent<Image>();
        starImg2 = transform.Find("star 2").GetComponent<Image>();
        starImg3 = transform.Find("star 3").GetComponent<Image>();
        starTxt1 = transform.Find("star 1 pts").GetComponent<Text>();
        starTxt2 = transform.Find("star 2 pts").GetComponent<Text>();
        starTxt3 = transform.Find("star 3 pts").GetComponent<Text>();
        totalTips = transform.Find("Tips Received").GetComponent<Text>();

        gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        // pause game
        Time.timeScale = 0f;

        UpdateUI();
        // Switch to game paused UI
        transform.Find("Header Text").GetComponent<Text>().text = "Game Paused";
        transform.Find("Resume Btn").GetComponent<Button>().gameObject.SetActive(true);

        // show UI
        gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void ShowEndScreen(bool wonStage)
    {
        // pause game
        Time.timeScale = 0f;

        // Switch to game end UI
        UpdateUI();
        transform.Find("Resume Btn").GetComponent<Button>().gameObject.SetActive(false);
        if (wonStage)
            transform.Find("Header Text").GetComponent<Text>().text = "Stage Cleared!";
        else
            transform.Find("Header Text").GetComponent<Text>().text = "Stage Failed.";

        // show UI
        gameObject.SetActive(true);

        // play popping animations on gold stars
        // TODO: bug - animation doesnt work cause time scale = 0
        //float playerScore = ScoreManager.score;
        //if (playerScore >= ScoreManager.threeStarsGoalStatic)
        //    starImg3.gameObject.GetComponent<Animator>().SetTrigger("Pop");
        //if (playerScore >= ScoreManager.twoStarsGoalStatic)
        //    starImg2.gameObject.GetComponent<Animator>().SetTrigger("Pop");
        //if (playerScore >= ScoreManager.goalScoreStatic)
        //    starImg1.gameObject.GetComponent<Animator>().SetTrigger("Pop");
    }

    public void UpdateUI()
    {
        totalTips.text = ScoreManager.score.ToString("C");              // currence format
        starTxt1.text  = ScoreManager.goalScoreStatic.ToString("f0");   // remove decimals
        starTxt2.text  = ScoreManager.twoStarsGoalStatic.ToString("f0");
        starTxt3.text  = ScoreManager.threeStarsGoalStatic.ToString("f0");

        float playerScore = ScoreManager.score;
        Color goldColor = new Color(1.0f, 0.78f, 0.21f);
        if (playerScore >= ScoreManager.threeStarsGoalStatic)
            starImg3.color = goldColor;
        if (playerScore >= ScoreManager.twoStarsGoalStatic)
            starImg2.color = goldColor;
        if (playerScore >= ScoreManager.goalScoreStatic)
            starImg1.color = goldColor;
    }
}
