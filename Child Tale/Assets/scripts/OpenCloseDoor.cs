using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool isOpen = false;
    private AudioSource audioSource;
    [SerializeField] AudioClip openDoor;
    [SerializeField] AudioClip closeDoor;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Active()
    {
        isOpen = !isOpen;
        animator.SetBool("Is Open", isOpen);
        if (isOpen)
            audioSource.PlayOneShot(openDoor);
        else
            audioSource.PlayOneShot(closeDoor);
        //StartCoroutine(DelayOnPlayRountine());
    }

    IEnumerator DelayOnPlayRountine()
    {
        yield return new WaitForSeconds(0.5f);
        if (isOpen)
            audioSource.PlayOneShot(openDoor);
        else
            audioSource.PlayOneShot(closeDoor);
    }
}
