using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using UnityEngine.Localization;

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

    private LocalizedString safe = new LocalizedString { TableReference = "safe" };

    void UpdateString(string translatedValue)
    {
        inputField.text = translatedValue;
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        safe.StringChanged += UpdateString;
    }

    public void checkAnswer()
    {
        if (isOpened) return;
        if (inputField.text == answer)
        {
            isOpened = true;
            animator.SetTrigger("Open");
            safe.TableEntryReference = "success";
            audioSource.Play();
        }
        else
        {
            safe.TableEntryReference = "error";
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
        PlayerData.instance.isSafeOpened = isOpened;
    }

    public void Load()
    {
        if (PlayerData.instance.isSafeOpened)
        {
            isOpened = true;
            animator.Play("openedsafe");
        }
        else
            animator.Play("New State");
    }
}
