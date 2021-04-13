using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

public class TV : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource speaker;

    private RawImage img;
    private VideoPlayer video;
    private PhotonView PV;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        img = GetComponentInChildren<RawImage>();
        img.enabled = false;
        PV = GetComponent<PhotonView>();
    }
    public void Active()
    {
        if (PhotonNetwork.OfflineMode)
            PlayVideo();
        else
            PV.RPC("PlayVideo", RpcTarget.All);
    }

    [PunRPC]
    void PlayVideo()
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
