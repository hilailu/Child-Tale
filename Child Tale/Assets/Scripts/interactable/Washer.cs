using Photon.Pun;
using UnityEngine;

public class Washer : MonoBehaviour, IInteractable
{
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;
    private bool isOpen = false;

    private PhotonView photonView;

    private void Start()
    {
        isOpen = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();
    }

    public void Active()
    {
        isOpen = !isOpen;
        if (PhotonNetwork.OfflineMode)
            playAnim(isOpen);
        else
            photonView.RPC("playAnim", RpcTarget.All, isOpen);
    }


    [PunRPC]
    void playAnim(bool bol)
    {
        isOpen = bol;
        animator.SetBool("IsOpened", bol);
        if (bol)
            audioSource.PlayOneShot(open);
        else
            audioSource.PlayOneShot(close);
    }
}
