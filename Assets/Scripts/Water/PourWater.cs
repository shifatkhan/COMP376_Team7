using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PourWater : MonoBehaviour
{
    [SerializeField]
    float fillSpeed = 0.5f;

    // covers up % of bar
    [SerializeField]
    float perfectZoneSize = 0.1f;
    [SerializeField]
    float failZoneSize = 0.1f;

    // the zone's length on the bar
    private float failZoneLength;
    private float perfectZoneLength;

    private Slider slider;
    private float sliderWidth = 200f;   // TODO get width programmatically
    private RectTransform perfectZoneImg;

    private WaterCupBar waterBarRef;

    private bool stopPouring = false;

    void Start()
    {
        waterBarRef = GameObject.Find("Water Cup Bar").GetComponent<WaterCupBar>();
        perfectZoneImg = transform.Find("Perfect Zone Image").GetComponent<RectTransform>();
        slider = GetComponent<Slider>();
        slider.value = 0;

        // find the starting point of each zone to calc which zone the skill check handle lands on
        failZoneLength = sliderWidth * failZoneSize;
        perfectZoneLength = sliderWidth * perfectZoneSize;

        // set up perfect zone on bar
        float perfectZoneCenter = sliderWidth - failZoneLength - (perfectZoneLength / 2);
        perfectZoneImg.anchoredPosition = new Vector2(perfectZoneCenter, 0);
        perfectZoneImg.sizeDelta = new Vector2(perfectZoneLength, 22f);

        // TODO temporarily disable player movement
    }


    void Update()
    {
        if (!stopPouring)
        {
            // TODO change input name
            if (Input.GetButton("Jump") || slider.value >= 1)
            {
                stopPouring = true;  // stops pouring water

                if (slider.value >= (sliderWidth - failZoneLength) / sliderWidth)
                    Debug.LogWarning("FAIL ZONE");
                else if (slider.value >= (sliderWidth - failZoneLength - perfectZoneLength) / sliderWidth)
                {
                    Debug.LogWarning("PERFECT ZONE");
                    waterBarRef.waterFilled();
                }
                else
                {
                    Debug.LogWarning("SUCCESS ZONE");
                    waterBarRef.waterFilled();
                }

                StartCoroutine(WaitBeforeDestroy());
            }

            else if (slider.value < 1)
                slider.value += fillSpeed * Time.deltaTime;
        }
    }

    // Show player's skill check result before destroy
    private IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(1);

        // TODO enable player movement
        Destroy(this.gameObject);
    }
}
