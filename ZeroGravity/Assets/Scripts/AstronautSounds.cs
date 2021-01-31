using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautSounds : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip choking;
    public AudioClip crash;
    public AudioClip oxygenPickup;
    public AudioClip boostPickup;
    public AudioClip thrust;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip audioclip)
    {
        audioSource.PlayOneShot(audioclip);
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public bool isPlaying()
    {
        return audioSource.isPlaying;
    }
}
