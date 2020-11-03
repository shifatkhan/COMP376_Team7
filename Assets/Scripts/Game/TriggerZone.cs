using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger Enter");
    }
    
    void OnTriggerStay(Collider col)
    {
        Debug.Log("Trigger Stay");
    }
    
    void OnTriggerExit(Collider col)
    {
        Debug.Log("Trigger Exit");
    }
}
