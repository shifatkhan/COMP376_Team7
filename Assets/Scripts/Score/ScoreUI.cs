using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates the score UI.
/// 
/// @author Thanh Tung Nguyen
/// </summary>
public class ScoreUI : MonoBehaviour
{
    [HideInInspector]
    public float goalScore = 0;

    private Text scoreTxt;
    private Slider slider;
    private Image checkMark;

    private float score = 0.0f;


    void Awake()
    {
        scoreTxt = transform.Find("Score Text").GetComponent<Text>();
        slider = transform.Find("Slider").GetComponent<Slider>();
        checkMark = transform.Find("Check Mark").GetComponent<Image>();

        scoreTxt.text = score.ToString("C");
        slider.value = 0.0f;
        checkMark.enabled = false;
    }

    public void NewScore(float newScore)
    {
        // update text
        score = newScore;
        scoreTxt.text = score.ToString("C");

        // update slider
        float progress = (score / goalScore);
        if (progress >= 1.0f)
            progress = 1.0f;
        slider.value = progress;

        // update status
        if (progress >= 1.0f)
            checkMark.enabled = true;
    }
}
