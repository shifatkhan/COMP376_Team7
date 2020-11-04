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
        //Spawn puddle at a random x, z location
        float xRandom = Random.Range(-8, 8);
        float y = 0.25f;
        float zRandom = Random.Range(-4.7f, 1);
        
        Vector3 spawnPosition = new Vector3(xRandom, y, zRandom);
        GameObject newPuddle = Instantiate(waterPuddle, spawnPosition, Quaternion.identity) as GameObject;
        newPuddle.transform.Rotate(0, Random.Range(0, 360), 0, Space.World); //Random y rotation
    }
}
