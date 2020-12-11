using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The water task manager attached to tables 
/// that have customers that drink water and 
/// players can refill them. 
/// 
/// @author Thanh Tung Nguyen
/// </summary>
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
        // find gameobjects & components
        screenCanvas = GameObject.Find("Canvas - Game");
        waterStateAnim = transform.Find("Table State UI/Bubble/Water State").GetComponent<Animator>();

        // start with full cup
        waterFilled();
    }

    void Update()
    {
        if (!skillChecking && isActive)
        {
            // slowly deplete water overtime
            if (waterCup > 0)
                waterCup -= (drainRate * 0.1f) * Time.deltaTime;

            // check water cup and indicate its state on UI
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
        // check first if player is attempting to interact with table
        if (Input.GetButton("Interact") && other.CompareTag("Player") && !skillChecking && waterCup <= 0.5f)
        {
            // then, check if player is holding a water jug
            // this happens later to prevent overcalling GetComponents
            GameObject objectHeld = other.gameObject.GetComponent<CheckNearbyInteraction>().getHeldObject();

            if (objectHeld != null && objectHeld.CompareTag("Water Jug"))
            {
                skillChecking = true;
                PlayerInputManager.enableMovement = false;

                // instantiate skill check prefab on screen
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
    }

    public void waterFilled()
    {
        skillChecking = false;
        waterCup = 1f;

        waterStateAnim.SetBool("LowOnWater", false);
        waterStateAnim.SetBool("EmptyCup", false);
    }

    //*** Draining Speed Difficulty ***//
    public void drinkSpeedEasy()    
    { drainRate = 0.06f; }
    public void drinkSpeedMedium()  
    { drainRate = 0.1f; }
    public void drinkSpeedHard()
    { drainRate = 0.12f; }
    //********************************//
}
