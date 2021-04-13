using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class SafeCode : MonoBehaviour, ISafeInteractive, ISaveable
{
    public TMP_Text inputField;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Transform pointForCamera;
    private PhotonView PV;
    private Camera cameraFromPlayer;

    private string answer = "12345";
    [HideInInspector] public int maxNumbers = 7;

    private Vector3 startPointCameraPos;
    private Quaternion startPointCameraQuat;

    public static bool isActive = false;
    [HideInInspector] public bool isOpened = false;
    private bool isSomebodyUse = false;


    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

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

    public void Active(Camera camera)
    {
        if (isSomebodyUse) return;

        if (!PhotonNetwork.OfflineMode)
            PV.RPC("SetUseable", RpcTarget.Others, true);


        startPointCameraPos = camera.transform.position;
        startPointCameraQuat = camera.transform.rotation;

        isActive = true;
        Cursor.visible = true;
        GameManager.isPaused = true;
        Cursor.lockState = CursorLockMode.None;

        cameraFromPlayer = camera;
    }

    private void Update()
    {
        if (isActive && GameManager.isPaused)
        {
            cameraFromPlayer.transform.position = Vector3.Lerp(cameraFromPlayer.transform.position, pointForCamera.position, Time.deltaTime * 2);
            cameraFromPlayer.transform.rotation = Quaternion.Lerp(cameraFromPlayer.transform.rotation, pointForCamera.rotation, Time.deltaTime * 2);
        }

        if (isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(aaa());
            GameManager.isPaused = false;

            cameraFromPlayer.transform.position = startPointCameraPos;
            cameraFromPlayer.transform.rotation = startPointCameraQuat;
        }
    }

    IEnumerator aaa()
    {
        yield return new WaitForSeconds(0.01f);
        isActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (!PhotonNetwork.OfflineMode)
            PV.RPC("SetUseable", RpcTarget.Others, false);
    }

    [PunRPC]
    void SetUseable(bool bol)
        => isSomebodyUse = bol;



    public void Save()
    {
        Debug.Log("Save Safe");
        PlayerPrefs.SetString("SafeJSON", JsonUtility.ToJson(this, true));
        PlayerPrefs.Save();
    }

    public void Load()
    {
        Debug.Log("Load Safe");
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("SafeJSON"), this);
        if (isOpened)
            animator.SetTrigger("Open");
        else
            animator.Play("New State");
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("SafeJSON");
    }
}
