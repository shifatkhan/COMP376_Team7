using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // class holds static reference to itself (singleton)
    static AudioManager audioManager;

    [Header("Music")]
    public AudioClip gameMusic;             // plays in-game music
    public AudioClip menuMusic;             // plays in menu or stage selection

    [Header("SFX")]
    public AudioClip menuClickClip;             // when click menu buttons
    public AudioClip waterCheckSuccessClip;     // when land water check in success zone
    public AudioClip waterCheckFailClip;        // when land water check in fail zone
    public AudioClip waterCheckPerfectClip;     // when land water check in perfect zone

    [Header("Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup playerGroup;
    public AudioMixerGroup voiceGroup;

    // Reference to the generated above Audio Source
    AudioSource musicSource;
    AudioSource sfxSource;
    AudioSource playerSource;
    AudioSource voiceSource;

    void Awake()
    {
        // make sure that: "there can only be ONE" manager
        if (audioManager != null && audioManager != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // make it persist between scenes
        audioManager = this;
        DontDestroyOnLoad(this.gameObject);

        // generate the audio sources
        musicSource  = gameObject.AddComponent<AudioSource>() as AudioSource;
        sfxSource    = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        voiceSource  = gameObject.AddComponent<AudioSource>() as AudioSource;

        // Assign each audio source to its own mixer group so its controlled by the audio mixer
        musicSource.outputAudioMixerGroup  = musicGroup;
        sfxSource.outputAudioMixerGroup    = sfxGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        voiceSource.outputAudioMixerGroup  = voiceGroup;
    }

    public static void PlayWaterCheckSuccess()
    {
        if (audioManager == null) return;

        audioManager.sfxSource.clip = audioManager.waterCheckSuccessClip;
        audioManager.sfxSource.Play();
    }

    public static void PlayWaterCheckFail()
    {
        if (audioManager == null) return;

        audioManager.sfxSource.clip = audioManager.waterCheckFailClip;
        audioManager.sfxSource.Play();
    }

    public static void PlayWaterCheckPerfect()
    {
        if (audioManager == null) return;

        audioManager.sfxSource.clip = audioManager.waterCheckPerfectClip;
        audioManager.sfxSource.Play();
    }
}
