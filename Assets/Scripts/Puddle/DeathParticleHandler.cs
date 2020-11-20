using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles a death particle. A death particle is a 
/// gameobject that gets intantiated when another gameobject gets destroyed.
/// For example, If someone dies, a gameobject with Blood particles will 
/// get instantiated.
/// 
/// This script will then destroy the particle after x seconds, or let the animation
/// handle when it's going to get destroyed.
/// 
/// @author ShifatKhan
/// </summary>
public class DeathParticleHandler : MonoBehaviour
{
    public float destroyTimer = 3f; // After x seconds, we destroy
    public bool destroyAfterAnimation = false; // Let animation handle destruction

    private float destroyTime; // To keep track of elapsed time.

    void Start()
    {
        destroyTime = Time.time;
    }

    void Update()
    {
        if(!destroyAfterAnimation && Time.time > destroyTime + destroyTimer)
        {
            DestroyParticle();
        }
    }

    public void DestroyParticle()
    {
        Destroy(gameObject);
    }
}
