using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceMeter : MonoBehaviour
{
    [SerializeField]
    float drainRate = 0.1f;

    private Animator tableStateAnim;
    private float patience = 1f;
    private bool isActive = false;

    void Awake()
    {
        tableStateAnim = transform.Find("Table State UI/Bubble/Customer Patience").GetComponent<Animator>();
    }

    void Update()
    {
        if (isActive)
        {
            if (patience > 0)
                patience -= (drainRate * 0.1f) * Time.deltaTime;

            // check patience state
            if (patience > 0.5 && patience <= 1)
            {
                tableStateAnim.SetBool("HighPatience", true);
                tableStateAnim.SetBool("MediumPatience", false);
                tableStateAnim.SetBool("LowPatience", false);
            }
            else if (patience > 0.2 && patience <= 0.5)
            {
                tableStateAnim.SetBool("HighPatience", false);
                tableStateAnim.SetBool("MediumPatience", true);
                tableStateAnim.SetBool("LowPatience", false);
            }
            else if (patience > 0 && patience <= 0.2)
            {
                tableStateAnim.SetBool("HighPatience", false);
                tableStateAnim.SetBool("MediumPatience", false);
                tableStateAnim.SetBool("LowPatience", true);
            }
            else if (patience <= 0)
            {
                tableStateAnim.SetBool("HighPatience", false);
                tableStateAnim.SetBool("MediumPatience", false);
                tableStateAnim.SetBool("LowPatience", false);
            }
        }
    }

    public void setActive(bool b)
    {
        isActive = b;
        drainSpeedEasy(); // default
    }

    public void increPatience(float val)
    {
        patience += val;
        if (patience > 1f)
            patience = 1f;
    }

    public void resetPatience()
    {
        patience = 1f;
    }

    public void drainSpeedEasy()
    {
        drainRate = 0.08f;
    }

    public void drainSpeedMedium()
    {
        drainRate = 0.16f;
    }

    public void drainSpeedHard()
    {
        drainRate = 0.4f;
    }
}
