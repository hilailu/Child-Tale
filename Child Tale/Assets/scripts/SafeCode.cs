using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeCode : MonoBehaviour, IInteractable
{
    public TMP_Text inputField;
    [SerializeField] Animator animator;
    [SerializeField] PlayerController player;
    [SerializeField] AudioSource audioSource;

    private string answer = "12345";
    public int maxNumbers = 7;

    public static bool isActive = false;


    public void checkAnswer()
    {
        if (inputField.text == answer)
        {
            animator.SetTrigger("Open");
            inputField.text = "Success";
            audioSource.Play();
        }
        else
        {
            inputField.text = "error";
        }
    }

    public void ClearInput()
        => inputField.text = string.Empty;

    public void Active()
    {
        isActive = !isActive;
        Cursor.visible = isActive;
        PlayerController.isPaused = isActive;
        Cursor.lockState = CursorLockMode.None;
    }

    //public void Update()
    //{
    //    if (isActive && Input.GetKeyDown(KeyCode.E))
    //    {
    //        isActive = !isActive;
    //        Cursor.visible = isActive;
    //        PlayerController.isPaused = isActive;
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //}
}
