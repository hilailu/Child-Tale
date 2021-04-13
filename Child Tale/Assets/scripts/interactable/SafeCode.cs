using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SafeCode : MonoBehaviour, IInteractable, ISaveable
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

    public bool isOpened = false;

    public void checkAnswer()
    {
        if (isOpened) return;
        if (inputField.text == answer)
        {
            isOpened = true;
            animator.SetTrigger("Open");
            inputField.text = "\u2713"; // ✓
            audioSource.Play();
        }
        else
        {
            inputField.text = "\u274C"; //❌
        }
    }

    public void ClearInput()
        => inputField.text = string.Empty;

    public void Active()
    {
        startPointCameraPos = player.cameraMain.transform.position;
        startPointCameraQuat = player.cameraMain.transform.rotation;

        isActive = true;
        Cursor.visible = true;
        GameManager.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (isActive && GameManager.isPaused)
        {
            player.cameraMain.transform.position = Vector3.Lerp(player.cameraMain.transform.position, pointForCamera.position, Time.deltaTime * 2);
            player.cameraMain.transform.rotation = Quaternion.Lerp(player.cameraMain.transform.rotation, pointForCamera.rotation, Time.deltaTime * 2);
        }

        if (isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(aaa());
            GameManager.isPaused = false;

            player.cameraMain.transform.position = startPointCameraPos;
            player.cameraMain.transform.rotation = startPointCameraQuat;
        }
    }

    IEnumerator aaa()
    {
        yield return new WaitForSeconds(0.01f);
        isActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Save()
    {
        PlayerData.instance.isSafeOpened = isOpened;
        Debug.Log("Save Safe");
        string player = JsonUtility.ToJson(PlayerData.instance, true);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", player);
    }

    public void Load()
    {
        Debug.Log("Load Safe");
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"), PlayerData.instance);
        if (PlayerData.instance.isSafeOpened)
        {
            isOpened = true;
            animator.SetTrigger("Open");
        }
        else
            animator.Play("New State");
    }
}
