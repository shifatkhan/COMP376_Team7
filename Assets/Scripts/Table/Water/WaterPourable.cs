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

    private Animator waterStateAnim;
    private float waterCup;
    private GameObject screenCanvas;

    private bool skillChecking = false;
    private bool isActive = false;

    void Awake()
    {
        screenCanvas = GameObject.Find("Canvas - Game");
        waterStateAnim = transform.Find("Table State UI/Bubble/Water State").GetComponent<Animator>();

        // start with full cup
        waterFilled();
    }

    void Update()
    {
        if (!skillChecking && isActive)
        {
            if (waterCup > 0)
                waterCup -= (drainRate * 0.1f) * Time.deltaTime;

            // check water cup state
            if (waterCup > 0.5 && waterCup <= 1)
            {
                waterStateAnim.SetBool("LowOnWater", false);
                waterStateAnim.SetBool("EmptyCup", false);
            }
            else if (waterCup > 0 && waterCup <= 0.5)
            {
                waterStateAnim.SetBool("LowOnWater", true);
                waterStateAnim.SetBool("EmptyCup", false);
            }
            else if (waterCup <= 0)
            {
                waterStateAnim.SetBool("EmptyCup", true);
                waterStateAnim.SetBool("LowOnWater", false);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Interact") && other.CompareTag("Player") && !skillChecking && waterCup <= 0.5f)
        {
            // check if player is holding a water jug
            GameObject objectHeld = other.gameObject.GetComponent<CheckNearbyInteraction>().getHeldObject();

            if (objectHeld != null && objectHeld.CompareTag("Water Jug"))
            {
                skillChecking = true;
                PlayerInputManager.enableMovement = false;

                Vector2 screenPos = new Vector2(Screen.width / 2, Screen.height / 2);
                Transform skillCheckObj = Instantiate(waterCheckPrefab, screenPos, waterCheckPrefab.rotation);
                skillCheckObj.SetParent(screenCanvas.transform);
                skillCheckObj.GetComponent<WaterCheckBar>().setTablePoured(this);
            }
        }
    }

    public void setActive(bool b)
    {
        isActive = b;
        drinkSpeedEasy(); // default
    }

    public void waterFilled()
    {
        skillChecking = false;
        waterCup = 1f;
    }

    public void drinkSpeedEasy()
    {
        drainRate = 0.04f;
    }

    public void drinkSpeedMedium()
    {
        drainRate = 0.08f;
    }

    public void drinkSpeedHard()
    {
        drainRate = 0.2f;
    }
}
