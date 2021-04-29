using UnityEngine;
using Photon.Pun;

public class Radio : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;
    private PhotonView PV;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
    }

    public void Active()
    {
        if (PhotonNetwork.OfflineMode)
            PlayMusic();
        else
            PV.RPC("PlayMusic", RpcTarget.All);
    }

    [PunRPC]
    void PlayMusic()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
        else
            audioSource.Stop();
    }
}
