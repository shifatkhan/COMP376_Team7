using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles buttons in Stage Select scene.
/// 
/// @author Thanh Tung Nguyen
/// </summary>
public class StageButton : MonoBehaviour
{
    [SerializeField]
    private string StageScene;
    [SerializeField] 
    private StageCode stageCode;

    void Awake()
    {
        int starsAchieved = PlayerPrefs.GetInt(stageCode.ToString(), 0);
        Color starColor = new Color(1f, 0.77f, 0.03f);

        // display how many stars this stage has
        if (starsAchieved >= 3)
            transform.Find("star 3").GetComponent<Image>().color = starColor;
        if (starsAchieved >= 2)
            transform.Find("star 2").GetComponent<Image>().color = starColor;
        if (starsAchieved >= 1)
            transform.Find("star 1").GetComponent<Image>().color = starColor;
    }

    public void LoadStage()
    {
        if (StageScene != "")
            SceneManager.LoadScene(StageScene);
    }
}
