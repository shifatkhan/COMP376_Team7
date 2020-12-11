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

    private Target targetIndicator;

    void Awake()
    {
        tableStateAnim = transform.Find("Table State UI/Bubble/Customer Patience").GetComponent<Animator>();
        targetIndicator = GetComponentInChildren<Target>();
    }

    private void Start()
    {
        targetIndicator.enabled = false;
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
                targetIndicator.enabled = false;
                tableStateAnim.SetBool("HighPatience", true);
                tableStateAnim.SetBool("MediumPatience", false);
                tableStateAnim.SetBool("LowPatience", false);
            }
            else if (patience > 0.2 && patience <= 0.5)
            {
                // Orange indicator
                targetIndicator.enabled = true;
                targetIndicator.ChangeColor(new Color(255f, 165, 0));

                tableStateAnim.SetBool("HighPatience", false);
                tableStateAnim.SetBool("MediumPatience", true);
                tableStateAnim.SetBool("LowPatience", false);
            }
            else if (patience > 0 && patience <= 0.2)
            {
                // Red indicator
                targetIndicator.enabled = true;
                targetIndicator.ChangeColor(Color.red);

                tableStateAnim.SetBool("HighPatience", false);
                tableStateAnim.SetBool("MediumPatience", false);
                tableStateAnim.SetBool("LowPatience", true);
            }
            else if (patience <= 0)
            {
                targetIndicator.enabled = false;
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

    public IEnumerator displayHeart()
    {
        isActive = false;
        tableStateAnim.SetBool("Heart", true);

        yield return new WaitForSeconds(3);

        tableStateAnim.SetBool("Heart", false);
        isActive = true;
    }
}
