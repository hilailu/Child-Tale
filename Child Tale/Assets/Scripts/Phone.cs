using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Phone : MonoBehaviour
{
    [SerializeField] private GameObject phoneUI;
    [SerializeField] private TMP_Text time;
    private Animator anim;
    public static bool phone = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        time.text = $"{CustomTime.hours:00}:{CustomTime.minutes:00}";
        if (!PlayerController.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                phone = !phone;
                anim.SetBool("PhoneOpen", phone);
                if (!phone)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    phoneUI.SetActive(false);
                    Invoke("Hide", 0.5f);
                }
                else
                {
                    Hide();
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            if (phone && Input.GetKeyDown(KeyCode.U))
            {
                phoneUI.SetActive(!phoneUI.activeSelf);
            }
        }
    }

    void Hide()
    {
        GetComponent<MeshRenderer>().enabled = phone;
    }
}