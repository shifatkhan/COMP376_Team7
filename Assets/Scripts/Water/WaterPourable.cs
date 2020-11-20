using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPourable : MonoBehaviour
{
    [SerializeField]
    Transform waterStatusPrefab;
    [SerializeField]
    Transform waterCheckPrefab;
    [SerializeField]
    float drainRate = 0.05f;

    private GameObject canvasRef;
    private Transform skillCheckObj;
    private Transform waterCupBarObj;
    private Slider waterCup;

    private bool skillChecking = false;

    void Start()
    {
        canvasRef = GameObject.Find("Canvas - Game");

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        waterCupBarObj = Instantiate(waterStatusPrefab, screenPos, Quaternion.identity);
        waterCupBarObj.SetParent(canvasRef.transform);

        waterCup = waterCupBarObj.GetComponent<Slider>();
        waterCup.value = 1f;
    }

    void Update()
    {
        // make water cup bar follow table when camera follows player

        if (skillChecking)
        {
            waterCupBarObj.gameObject.SetActive(false);

            // TODO temporarily disable player movement

            // enable water draining again if done skill check
            if (skillCheckObj == null)
                skillChecking = false;
                // TODO enable player movement again
        }
        else if (!skillChecking)
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

                Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
                skillCheckObj = Instantiate(waterCheckPrefab, screenPos, waterCheckPrefab.rotation);
                skillCheckObj.SetParent(canvasRef.transform);
                skillCheckObj.GetComponent<WaterCheckBar>().setTablePoured(this);
            }
        }
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
