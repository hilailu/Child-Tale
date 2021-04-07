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
    [SerializeField] Transform pointForCamera;

    private string answer = "12345";
    public int maxNumbers = 7;

    public static bool isActive = false;
    public static bool isMoving = false;


    private Vector3 startPointCameraPos;
    private Quaternion startPointCameraQuat;
    private Transform startPointCamera;



    private void Start()
    {
        isActive = false;
    }

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
        startPointCameraPos = player.cameraMain.transform.position;
        startPointCameraQuat = player.cameraMain.transform.rotation;

        isActive = true;
        Cursor.visible = isActive;
        PlayerController.isPaused = isActive;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (isActive && PlayerController.isPaused)
        {
            player.cameraMain.transform.position = Vector3.Lerp(player.cameraMain.transform.position, pointForCamera.position, Time.deltaTime * 2);
            player.cameraMain.transform.rotation = Quaternion.Lerp(player.cameraMain.transform.rotation, pointForCamera.rotation, Time.deltaTime * 2);
        }

        if (isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(aaa());
            PlayerController.isPaused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            player.cameraMain.transform.position = startPointCameraPos;
            player.cameraMain.transform.rotation = startPointCameraQuat;
        }
    }

    IEnumerator aaa()
    {
        yield return new WaitForSeconds(0.1f);
        isActive = false;
    }
}
