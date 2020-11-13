using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is only responsible for a radial progress bar.
/// Change the value of `progress` from 0 to 100 to update the bar.
/// 
/// @author ShifatKhan
/// </summary>
public class RadialProgressBar : MonoBehaviour
{
    public Image bgImage;
    public Text progressText;

    [Range(0,100)]
    public int progress = 0;

    private void Update()
    {
        float amount = progress / 100.0f;
        bgImage.fillAmount = amount;

        progressText.text = string.Format("{0}%", progress);
    }
}
