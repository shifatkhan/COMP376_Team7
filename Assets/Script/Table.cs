using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public bool isOccupied;                    //If the table already has a group on it
    public void Start()
    {
        transform.Find("Cube").gameObject.SetActive(false);

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        //gameObject.transform.GetChild(2).gameObject.SetActive(false);
     
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Make table so it occupied
    // As for the prototype, it just changed the color to red --> means O-C-C-U-P-I-E-D
    public void EnableCustomers()
    {

        //gameObject.transform.GetChild(2).gameObject.SetActive(true);
        transform.Find("Cube").gameObject.SetActive(true);
        isOccupied = true;

    }

    public void Seated()
    {
        transform.Find("Cube").gameObject.SetActive(false);
    }
}

