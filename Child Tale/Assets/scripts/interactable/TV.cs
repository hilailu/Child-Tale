using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TV : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource speaker;

    private RawImage img;
    private VideoPlayer video;
    
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        img = GetComponentInChildren<RawImage>();
        img.enabled = false;
    }
    public void Active()
    {
        if (!video.isPlaying)
        {
            img.enabled = true;
            video.Play();
            speaker.Play();
        }
        else
        {
            img.enabled = false;
            video.Stop();
            speaker.Stop();
        }
    }
}
