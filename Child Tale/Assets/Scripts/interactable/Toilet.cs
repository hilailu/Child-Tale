using UnityEngine;
using Photon.Pun;

public class Toilet : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;
    private PhotonView PV;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
    }
    public void Active()
    {
        if (PhotonNetwork.OfflineMode)
            PlaySounde();
        else
            PV.RPC("PlaySounde", RpcTarget.All);
    }

    [PunRPC]
    void PlaySounde()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
