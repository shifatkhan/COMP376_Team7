using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHazards : MonoBehaviour
{
    public bool keepSpawning = true;
    public float minSpawnTime = 20f;
    public float maxSpawnTime = 30f;

    [SerializeField]
    private GameObject puddlePrefab; //Prefab to spawn

    [SerializeField]
    private GameObject[] spawnPoints;

    void Start() 
    {
        // Start Spawning Puddle
        StartCoroutine(SpawnAtIntervals());
    }

    IEnumerator SpawnAtIntervals()
    {
        // Repeat until keepSpawning == false or this GameObject is disabled/destroyed.
        while (keepSpawning)
        {
            // Put this coroutine to sleep until the next spawn time.
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            // Now it's time to spawn again.
            SpawnPuddle();
        }
    }

    void SpawnPuddle()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(puddlePrefab, spawnPoints[spawnIndex].transform.position, Quaternion.identity);
    }
}
