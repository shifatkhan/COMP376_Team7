using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleManagerJames : MonoBehaviour
{
    public bool keepSpawning = true;
    public float minSpawnTime = 20f;
    public float maxSpawnTime = 30f;

    [SerializeField]
    private GameObject puddlePrefab; //Prefab to spawn

    [SerializeField]
    private GameObject[] spawnPoints;

    private static PuddleManagerJames _instance;

    public static PuddleManagerJames Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PuddleManagerJames>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(PuddleManagerJames).Name;
                    _instance = go.AddComponent<PuddleManagerJames>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


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
        print(spawnIndex);
        Instantiate(puddlePrefab, spawnPoints[spawnIndex].transform.position, Quaternion.identity);
    }
}
