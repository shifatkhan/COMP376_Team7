using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
