using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Delete this file.
[System.Obsolete("It is not best practice to spawn something in OnDestroy()", true)]
public class DeathParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;

    private void OnDestroy()
    {
        if (this.enabled)
        {
            Instantiate(prefabToSpawn, transform.position, transform.rotation);
        }
    }
}
