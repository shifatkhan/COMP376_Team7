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

public class StageSelection : MonoBehaviour
{
    void Awake()
    {
        //if (!Application.isEditor)
        //    PlayerPrefs.DeleteAll();

        Time.timeScale = 1.0f;
        string[] stageCodes = System.Enum.GetNames(typeof(StageCode));

        //************************** LEVEL 1 **************************//
        // get total stars
        int totalStarsThisLevel = PlayerPrefs.GetInt(stageCodes[0], 0) +
                                  PlayerPrefs.GetInt(stageCodes[1], 0) +
                                  PlayerPrefs.GetInt(stageCodes[2], 0) ;
        // if a player gets all stars for a level, display a medal
        if (totalStarsThisLevel == 9)
            transform.Find("Level 1/Medal").gameObject.SetActive(true);
        // Unlock a level based on number of stars it has
        if (totalStarsThisLevel >= 6)
            transform.Find("Level 2 Cover").gameObject.SetActive(false);

        //************************** LEVEL 2 **************************//
        // get total stars
        totalStarsThisLevel = PlayerPrefs.GetInt(stageCodes[3], 0) +
                              PlayerPrefs.GetInt(stageCodes[4], 0) +
                              PlayerPrefs.GetInt(stageCodes[5], 0) ;
        // if a player gets all stars for a level, display a medal
        if (totalStarsThisLevel == 9)
            transform.Find("Level 1/Medal").gameObject.SetActive(true);
        // Unlock a level based on number of stars it has
        //if (totalStarsThisLevel >= 6)
            //transform.Find("Level 2 Cover").gameObject.SetActive(false);
    }
}
