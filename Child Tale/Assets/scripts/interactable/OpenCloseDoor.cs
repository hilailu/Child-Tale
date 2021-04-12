using Photon.Pun;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour, IInteractable
{
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] AudioClip openDoor;
    [SerializeField] AudioClip closeDoor;
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
        animator.SetBool("Is Open", bol);
        if (bol)
            audioSource.PlayOneShot(openDoor);
        else
            audioSource.PlayOneShot(closeDoor);
    }
}
