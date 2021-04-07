using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExit : MonoBehaviour, IInteractable
{
    [SerializeField] InventoryManager inventory;
    [SerializeField] AudioClip knobSounde;
    [SerializeField] AudioClip openDoorSounde;
    [SerializeField] Animator animator;
    [SerializeField] Item key;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Active()
    {
        if (inventory.items.Contains(key))
        {
            animator.SetTrigger("Game Over");
            PlayerController.isPaused = true;
            audioSource.PlayOneShot(openDoorSounde);
        }
        else
        {
            animator.SetTrigger("Knob");
            audioSource.PlayOneShot(knobSounde);
        }
    }
}
