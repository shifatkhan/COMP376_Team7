using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCheckBar : MonoBehaviour
{
    [Header("Water check vars")]
    [SerializeField]
    float fillSpeed = 0.5f;

    // covers up % of bar
    [SerializeField]
    float perfectZoneSize = 0.05f;
    [SerializeField]
    float failZoneSize = 0.05f;

    [Header("Tip modifiers")]
    [SerializeField]
    private float addTip = 0.15f;
    [SerializeField]
    private float substractTip = 0.1f;

    // the zone's length on the bar
    private float failZoneLength;
    private float perfectZoneLength;

    private Slider slider;
    private float sliderWidth;
    private RectTransform perfectZoneImg;
    private Button spaceIndicator;

    private WaterPourable tableBeingPoured;
    private Table tableRef;
    private bool stopPouring = false;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
        perfectZoneImg = transform.Find("Perfect Zone Image").GetComponent<RectTransform>();
        spaceIndicator = transform.Find("Indicator").GetComponent<Button>();

        // find the starting point of each zone to calc which zone the skill check handle lands on
        sliderWidth = GetComponent<RectTransform>().sizeDelta.x;
        failZoneLength = sliderWidth * failZoneSize;
        perfectZoneLength = sliderWidth * perfectZoneSize;

        // set up perfect zone on bar
        float perfectZoneCenter = sliderWidth - failZoneLength - (perfectZoneLength / 2);
        perfectZoneImg.anchoredPosition = new Vector2(perfectZoneCenter, 0);
        perfectZoneImg.sizeDelta = new Vector2(perfectZoneLength, 34f);
    }


    void Update()
    {
        if (slider.value >= 0.5)
            spaceIndicator.interactable = true;

        if (!stopPouring)
        {
            if ((slider.value >= 0.5 && Input.GetButton("Hit Water Check")) || slider.value >= 1)
            {
                stopPouring = true;  // stops pouring water

                float perfZoneStartPoint = sliderWidth - failZoneLength - perfectZoneLength;

                if (slider.value < (perfZoneStartPoint / sliderWidth))
                {
                    // SUCCESS ZONE
                    tableBeingPoured.waterFilled();
                    AudioManager.PlayWaterCheckSuccess();
                }
                else if (slider.value <= (perfZoneStartPoint + perfectZoneLength) / sliderWidth)
                {
                    // PERFECT ZONE
                    tableBeingPoured.waterFilled();
                    AudioManager.PlayWaterCheckPerfect();

                    tableRef.displayHeart();
                    tableRef.AddBonusMultiplier(addTip);
                }
                else
                {
                    // FAIL ZONE
                    tableBeingPoured.waterFilled();
                    AudioManager.PlayWaterCheckFail();

                    tableRef.AddBonusMultiplier(substractTip);
                }
                
                // indicate where they landed the water check before destroying
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

        PlayerInputManager.enableMovement = true;
        Destroy(this.gameObject);
    }

    public void setTablePoured(WaterPourable table)
    {
        tableBeingPoured = table;
        this.tableRef = table.GetComponent<Table>();
    }
}
