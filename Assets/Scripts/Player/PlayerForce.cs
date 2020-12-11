using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the force the player is able to send to the object. The longer
/// he holds left_mouse_button the stronger the force. The player clicks on the screen the direction he wants to send the force.
/// @author TaqiHaque
/// </summary>

public class PlayerForce : MonoBehaviour
{
    public LayerMask clickMask; //Ground layer
    float holdDownStartTime;
    float holdDownTime = 0;
    bool canceled = false;
    Vector3 clickedPos; //Position player clicked on screen
    float extraForce; //Extra force by holding longer

    //Scripts
    PlayerInputManager playerInput;
    public PlayerForceUI forceUI;
    CheckNearbyInteraction nearbyObjectScript;

    [SerializeField] 
    float lowForce, medForce, hardForce, maxForce, yForce;

    void Start()
    {
        playerInput = PlayerInputManager.instance;

        nearbyObjectScript = GetComponent<CheckNearbyInteraction>();

        forceUI.SetMaxTime(1.8f); //Max time is 1.8seconds for force bar
    }

    void Update()
    {
        if(nearbyObjectScript.getHeldObject() != null && PlayerInputManager.enableThrow)
        {
            //Player presses mouse left button
            ForceStart();

            //Holds it
            ForceHold();
            
            //If Canceled it by right clicking
            ForceCancel();

            //Releases mouse left button
            ForceLaunch();
        }
    }

    void ForceStart()
    {
        //Mouse Down, start holding
        if(playerInput.forceStart)
        {
            forceUI.ShowUI();
            canceled = false;

            holdDownStartTime = Time.time;

            
        }
    }

    void ForceHold()
    {
        //left Mouse still down, show force
        if(playerInput.forceHold)
        {
            holdDownTime = Time.time - holdDownStartTime;
            forceUI.SetCurrentTime(holdDownTime);
            //print(holdDownTime );
        }
    }

    void ForceLaunch()
    {
        if(playerInput.forceLaunch && canceled == false)
        {
            forceUI.HideUI();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100f, clickMask))
            {
                clickedPos = hit.point; //Position player clicked
            }     

            //Low force
            if(holdDownTime <= 0.6f)
            {
                extraForce = lowForce;
            }
            //Medium force
            else if(holdDownTime <=1.2f)
            {
                extraForce = medForce;
            }
            //Hard force
            else if(holdDownTime <=1.7)
            {
                extraForce = hardForce;
            }
            else if(holdDownTime >1.7)
            {
                extraForce = maxForce;
            }

            print(extraForce);
            
            GameObject heldObject = nearbyObjectScript.getHeldObject();
            nearbyObjectScript.ObjectDown();

            Vector3 force = new Vector3(clickedPos.x-transform.position.x, yForce, clickedPos.z-transform.position.z); //Vector is difference between mouse click position and player's position
            //print(force + " " + force.normalized);      
            
            heldObject.GetComponent<Rigidbody>().AddForce(extraForce * force.normalized, ForceMode.Impulse); //Normalize the force and multiply it by an extra force depending on hold time
        }
    }

    void ForceCancel()
    {
        //Left mouse still down, but right mouse click to cancel
        if(playerInput.forceCancel)
        {
            forceUI.HideUI();
            canceled = true;
        }
    }
}
