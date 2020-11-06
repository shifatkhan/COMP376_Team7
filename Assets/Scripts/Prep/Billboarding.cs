using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will billboard a UI element in world space.
/// 
/// @author ShifatKhan
/// </summary>
public class Billboarding : MonoBehaviour
{
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
