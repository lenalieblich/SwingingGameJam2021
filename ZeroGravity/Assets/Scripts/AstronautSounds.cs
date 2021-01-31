using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautSounds : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip choking;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip audioclip)
    {
        audioSource.PlayOneShot(audioclip);
    }
}
