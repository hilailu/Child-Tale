using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Active()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
