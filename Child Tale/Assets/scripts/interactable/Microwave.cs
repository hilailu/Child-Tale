using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Microwave : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;
    private Animator animator;
    private PhotonView PV;
    [SerializeField] AudioClip openSounde;
    [SerializeField] AudioClip workingSounde;
    private bool isWorking;

    private void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        animator = GetComponentInParent<Animator>();
        PV = GetComponentInParent<PhotonView>();
        isWorking = false;
    }

    public void Active()
    {
        if (isWorking) return;

        if (PhotonNetwork.OfflineMode)
            playAnim();
        else
            PV.RPC("playAnim", RpcTarget.All);
    }

    [PunRPC]
    void playAnim()
        => StartCoroutine(WorkingMicrowaveRoutine());

    IEnumerator WorkingMicrowaveRoutine()
    {
        isWorking = true;
        audioSource.PlayOneShot(workingSounde);
        yield return new WaitForSeconds(60f);
        animator.SetBool("IsOpen", isWorking);
        audioSource.PlayOneShot(openSounde);
        Destroy(this);
    }
}
