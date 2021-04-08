using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Active()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
        else 
            audioSource.Stop();
    }
}
