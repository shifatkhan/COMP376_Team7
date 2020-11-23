using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPourable : MonoBehaviour
{
    [SerializeField]
    Transform waterCheckPrefab;
    [SerializeField]
    float drainRate = 0.05f;

    private Transform skillCheckObj;
    private Transform waterCupBarObj;
    private Slider waterCup;
    private GameObject screenCanvas;

    private bool skillChecking = false;
    private bool isActive = false;

    void Awake()
    {
        screenCanvas = GameObject.Find("Canvas - Game");
        waterCupBarObj = transform.Find("Water Status Position/Water Status Canvas/Water Cup Bar");

        // start with full cup
        waterCup = waterCupBarObj.GetComponent<Slider>();
        waterCup.value = 1f;
    }

    void Update()
    {
        if (skillChecking)
        {
            waterCupBarObj.gameObject.SetActive(false);

            // TODO temporarily disable player movement

            // enable water draining again if done skill check
            if (skillCheckObj == null)
                skillChecking = false;
                // TODO enable player movement again
        }
        else if (!skillChecking && isActive)
        {
            waterCupBarObj.gameObject.SetActive(true);

            if (waterCup.value > 0)
                waterCup.value -= (drainRate * 0.1f) * Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Interact") && other.CompareTag("Player") && !skillChecking && waterCup.value <= 0.5f)
        {
            // check if player is holding a water jug
            if (other.gameObject.GetComponent<CheckNearbyInteraction>().getHeldObject().CompareTag("Water Jug"))
            {
                skillChecking = true;

                Vector2 screenPos = new Vector2(Screen.width / 2, Screen.height / 2);
                skillCheckObj = Instantiate(waterCheckPrefab, screenPos, waterCheckPrefab.rotation);
                skillCheckObj.SetParent(screenCanvas.transform);
                skillCheckObj.GetComponent<WaterCheckBar>().setTablePoured(this);
            }
        }
    }

    public void startDrinking()
    {
        waterCupBarObj.gameObject.SetActive(true);
        isActive = true;
        drinkSpeedEasy(); // default
    }

    public void waterFilled()
    {
        waterCup.value = 1f;
    }

    public void drinkSpeedEasy()
    {
        drainRate = 0.02f;
    }

    public void drinkSpeedMedium()
    {
        drainRate = 0.04f;
    }

    public void drinkSpeedHard()
    {
        drainRate = 0.1f;
    }
}
