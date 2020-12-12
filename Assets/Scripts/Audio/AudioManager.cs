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
    public AudioClip menuMusic;             // plays in menu music
    public AudioClip ambianceSound;         // plays in cafe ambiance

    [Header("SFX")]
    public AudioClip menuClickClip;             // when click menu buttons
    public AudioClip waterCheckSuccessClip;     // when land water check in success zone
    public AudioClip waterCheckFailClip;        // when land water check in fail zone
    public AudioClip waterCheckPerfectClip;     // when land water check in perfect zone
    public AudioClip playerSlipClip;            // when the player slips
    public AudioClip playerThrowClip;           // when the player throws an object
    public AudioClip playerDropClip;           // when the player throws an object
    public AudioClip playerPickupClip;          // when the player Pickup an object
    public AudioClip[] walkStepClips;           // The footstep sound effects
    public AudioClip paySlipClip;          // When customer pay sfx


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

    private void Start()
    {
        PlayMenuMusic();
        PlayGameMusic();
        PlayAmbianceAudio();
    }

    public static void PlayMenuMusic()
    {
        if (audioManager == null || audioManager.menuMusic == null) return;

        audioManager.musicSource.clip = audioManager.menuMusic;
        audioManager.musicSource.loop = true;
        audioManager.musicSource.Play();
    }

    public static void PlayGameMusic()
    {
        if (audioManager == null || audioManager.gameMusic == null) return;

        audioManager.musicSource.clip = audioManager.gameMusic;
        audioManager.musicSource.loop = true;
        audioManager.musicSource.Play();
    }

    public static void PlayAmbianceAudio()
    {
        if (audioManager == null || audioManager.ambianceSound == null) return;

        audioManager.voiceSource.clip = audioManager.ambianceSound;
        audioManager.voiceSource.loop = true;
        audioManager.voiceSource.Play();
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

    public static void PlayPlayerSlip()
    {
        if (audioManager == null) return;
        if(audioManager.playerSource.isPlaying && audioManager.playerSource.clip == audioManager.playerSlipClip) return;

        audioManager.playerSource.clip = audioManager.playerSlipClip;
        audioManager.playerSource.Play();
    }
    
    public static void PlayPlayerThrow()
    {
        if (audioManager == null) return;

        audioManager.playerSource.clip = audioManager.playerThrowClip;
        audioManager.playerSource.Play();
    }

    public static void PlayPlayerDrop()
    {
        if (audioManager == null) return;

        audioManager.playerSource.clip = audioManager.playerDropClip;
        audioManager.playerSource.Play();
    }

    public static void PlayPlayerPickup()
    {
        if (audioManager == null) return;

        audioManager.playerSource.clip = audioManager.playerPickupClip;
        audioManager.playerSource.Play();
    }

    public static void PlayFootstepAudio()
    {
        //If there is no current AudioManager or the player source is already playing
        //a clip, exit 
        if (audioManager == null || audioManager.playerSource.isPlaying)
            return;

        //Pick a random footstep sound
        int index = Random.Range(0, audioManager.walkStepClips.Length);

        //Set the footstep clip and tell the source to play
        audioManager.playerSource.clip = audioManager.walkStepClips[index];
        audioManager.playerSource.Play();
    }

    public static void PlayPayAudio()
    {
        if (audioManager == null) return;

        audioManager.sfxSource.clip = audioManager.paySlipClip;
        audioManager.sfxSource.Play();
    }
}
