using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    //Add a cube as child to the tables, set is active(false) by default.
    //Whenever you run the code and choose a table, do SetActive(true)
    //[9:42 PM]
    //like we just need to show that the table is active you know

    private bool seated = false;                    //If the table already has a group on it
    private float timer = 5;                        //Timers when customers are seated

    //[SerializeField] private Material myMaterial;    //Change the color of the table
    //[SerializeField] private Renderer myObject;    //Change the color of the table

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Start()
    {
        

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Make table so it occupied
    // As for the prototype, it just changed the color to red --> means O-C-C-U-P-I-E-D
    public void EnableCustomers()
    {
        //myMaterial.color = Color.red;
        //myObject.material.color = Color.red;
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        gameObject.transform.GetChild(2).gameObject.SetActive(true);

        seated = true;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}