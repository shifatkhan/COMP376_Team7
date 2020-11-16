using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHazards : MonoBehaviour
{
    public GameObject waterPuddle; //Prefab water

    void Start() 
    {
        //Spawn Puddle
        InvokeRepeating("SpawnPuddle", Random.Range(10, 15), Random.Range(20, 30)); //1st puddle will appear between 10 to 15 sec, the following puddles will appear every 20 to 30 seconds
        
    }
    void SpawnPuddle()
    {
        Vector3 spawnPosition;
        Collider[] objectHit;

        float xRandom;
        float y;
        float zRandom;
        
        //Repeat calculation of position until it is in a position that does not hold another object (floor, table, etc.). Spill should not be spawned on top of another object.
        do
        {
            //Spawn puddle at a random x, z location
            xRandom = Random.Range(-9, 23);
            y = 0.25f;
            zRandom = Random.Range(-15f, 1.5f);
   
            spawnPosition = new Vector3(xRandom, y, zRandom); 

            objectHit = Physics.OverlapSphere(spawnPosition, 0.2f);
            //Debug.Log(objectHit.Length);
        }while(objectHit.Length != 0);

        GameObject newPuddle = Instantiate(waterPuddle, spawnPosition, Quaternion.identity) as GameObject; 
        newPuddle.transform.Rotate(0, Random.Range(0, 360), 0, Space.World); //Random y rotation
    }
}
