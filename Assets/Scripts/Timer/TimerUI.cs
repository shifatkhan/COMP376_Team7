using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private Text text;
    private int timeLeft;
    private int minDigit;
    private int seconds;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    int getMin(int a)
    {
        return a / 60;
    }

    int getSecRemain(int a)
    {
        return a % 60;
    }

    // Timer countdown is updated each frame
    void Update()
    {
        
        timeLeft = (int)(GameManager.timeLimitStatic - GameManager.currentTimeStatic);

        text.text = getMin(timeLeft).ToString();
        text.text += ":";
        if(getSecRemain(timeLeft) <= 9)
        {
            text.text = text.text + "0" + getSecRemain(timeLeft);
        }
        else
        {
            text.text += getSecRemain(timeLeft);
        }
        
    }
}
