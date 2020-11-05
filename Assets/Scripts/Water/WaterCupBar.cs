using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCupBar : MonoBehaviour
{
    [SerializeField]
    float drainRate = 0.1f;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value > 0)
            slider.value -= drainRate * Time.deltaTime;
    }

    public void waterFilled()
    {
        slider.value = 1f;
    }
}
