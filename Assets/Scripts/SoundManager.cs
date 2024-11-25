using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton pattern for easy global access
    public static SoundManager Instance;

    // Reference to sound effects AudioSource
    public AudioSource sfxSource;

    // Array to store different sound effects (you can add more as needed)
    public AudioClip[] soundEffects;

    private void Awake()
    {
        // If there is already an instance, destroy this one
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Ensure that the sound manager persists between scenes
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < soundEffects.Length)
        {
            sfxSource.PlayOneShot(soundEffects[clipIndex]);
        }
    }
}
