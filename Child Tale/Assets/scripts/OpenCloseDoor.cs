using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Active()
    {
        isOpen = !isOpen;
        animator.SetBool("Is Open", isOpen);
    }
}
