using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PourWater : MonoBehaviour
{
    [SerializeField]
    float fillSpeed = 0.5f;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

    
    void Update()
    {
        if (slider.value < 100)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }

}
