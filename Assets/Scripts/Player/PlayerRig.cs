using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerRig : MonoBehaviour
{
    public ChainIKConstraint rightArm = null;
    public Transform target = null;

    void Start() 
    {
        rightArm.weight = 0;
    }

    public void RaiseArm()
    {
        rightArm.weight = 1;
    }

    public void LowerArm()
    {
        rightArm.weight = 0;
    }

}
