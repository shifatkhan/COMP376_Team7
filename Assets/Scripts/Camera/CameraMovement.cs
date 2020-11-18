using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    CinemachineFramingTransposer vcamFT;

    GameObject[] player;

    void Start()
    {
        //vcam = GetComponent<CinemachineFramingTransposer>();
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcamFT = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();

        //Get Player object
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(vcamFT.m_ScreenX);
        //Debug.Log(vcamFT.m_DeadZoneWidth);
        //Debug.Log(player[0].transform.position);

        //Within distance of left wall
        if(player[0].transform.position.x >=0.5f && player[0].transform.position.x<=12.4f)
        {
            //vcamFT.m_ScreenX = 0.5f;
        }
        else
        {
            //vcamFT.m_ScreenX = calculateLinearInterpolation(player[0].transform.position.x);
        }

    }

    float calculateLinearInterpolation(float x)
    {
        float y; //ScreenX value for position x

        //Linear interpolation formula: y = y1 + (x-x1) * (y2-y1)/(x2-x1_)
        //(-11.2, 0.039) and (-5.4, 0.275)
        //(14.19, 0.569)
        y = (1.5f + 0.039f) + (x - (-11.2f)) * ((0.275f - 0.039f)/(-5.4f + 1.5f - (-11.2f + 1.5f))); //y range is [-1.5, 1.5], so do +1.5 to make range [0, 3]. Subtract by 1.5 at the end to return y to the real value
        //y = (1.5f + y)/3;

        return y-1.5F;
    }
}
