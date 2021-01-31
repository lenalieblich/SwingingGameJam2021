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
}
