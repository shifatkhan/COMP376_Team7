using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerForceUI : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    void Start()
    {
        //UI initially not shown
        HideUI();
    }

    public void SetCurrentTime(float time)
    {
        slider.value = time;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxTime(float time)
    {
        slider.value = time;
        fill.color = gradient.Evaluate(1f);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
}
