using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Starts the audio player on loop.
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    static AudioSource audioSrc;
    public static float initialVolume { get; private set; }

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        initialVolume = audioSrc.volume;
    }

    public static void PlayerMusic()
    {
        audioSrc.Play();
    }

    public static void SetSpeed(float speed)
    {
        audioSrc.pitch = speed;
    }

    public static void SetVolume(float vol)
    {
        audioSrc.volume = vol;
    }
}
