using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;

    private void OnDestroy()
    {
        Instantiate(prefabToSpawn, transform.position, transform.rotation);
    }
}
