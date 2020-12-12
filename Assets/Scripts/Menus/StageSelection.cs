using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enums of stage codes to save stars and scores
/// </summary>
public enum StageCode
{
    Level1_Stage1, Level1_Stage2, Level1_Stage3,
    Level2_Stage1, Level2_Stage2, Level2_Stage3,
    Level3_Stage1, Level3_Stage2, Level3_Stage3,
    Level4_Stage1, Level4_Stage2, Level4_Stage3
}

/// <summary>
/// Determines which levels are unlocked, how 
/// many stars each stages have, etc, from PlayerPrefs
/// 
/// @author Thanh Tung Nguyen
/// </summary>
public class StageSelection : MonoBehaviour
{
    private int numLevels = 4;  // each level has 3 stages
    private int numStages = 3;  // each stage belongs to one level
    private Color starColor = new Color(1f, 0.77f, 0.03f);

    void Awake()
    {
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1.0f;
        string[] stageCodes = System.Enum.GetNames(typeof(StageCode));
        GameObject[] stars = GameObject.FindGameObjectsWithTag("StageStar");

        for (int lvl=0; lvl < numLevels; lvl++)
        {
            int[] numStars = new int[3];

            for (int stg=0; stg < numStages; stg++)
            {
                // get number of stars for each level
                numStars[stg] = PlayerPrefs.GetInt(stageCodes[lvl * numStages + stg], 0);

                // display how many stars each stage has
                int starsPerStage = 3;
                int starsPerLevel = 9;
                for(int i=0; i < numStars[stg]; i++)
                {
                    stars[lvl * starsPerLevel + stg * starsPerStage + i]
                        .GetComponent<Image>().color = starColor;
                }
            }

            // Unlock a level based on number of stars it has (except last level)
            if (lvl < (numLevels-1) && numStars[0] + numStars[1] + numStars[2] >= 6)
                transform.Find("Level " + (lvl + 2) + " Cover").gameObject.SetActive(false);

            // if a player gets all stars for a level, display a medal
            if (numStars[0] + numStars[1] + numStars[2] == 9)
                transform.Find("Level " + (lvl + 1) + "/Medal").gameObject.SetActive(true);
        }    
    }
}
