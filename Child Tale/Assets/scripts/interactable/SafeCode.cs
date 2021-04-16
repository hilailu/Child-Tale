using System.Collections;
using UnityEngine;
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

    public string answer = "12345";
    [HideInInspector] public int maxNumbers = 7;

    private Vector3 startPointCameraPos;
    private Quaternion startPointCameraQuat;

    [HideInInspector] public bool isOpened = false;
    public static bool isActive = false;
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
        GameManager.isPaused = true;
        GameManager.instance.CursorView(true);

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
            StartCoroutine(ForPressEscRoutine());
            GameManager.isPaused = false;

            cameraFromPlayer.transform.position = startPointCameraPos;
            cameraFromPlayer.transform.rotation = startPointCameraQuat;
        }
    }


    IEnumerator ForPressEscRoutine()
    {
        yield return new WaitForSeconds(0.01f);
        isActive = false;
        GameManager.instance.CursorView(false);

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
