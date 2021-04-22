using System.Collections;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.Localization;

public class SafeCode : MonoBehaviour, IPlayerInteractive, ISaveable
{
    public TMP_Text inputField;
    static Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Transform pointForCamera;
    private PhotonView PV;
    private Camera cameraFromPlayer;

    public string answer = "41852";
    [HideInInspector] public int maxNumbers = 7;

    private Vector3 startPointCameraPos;
    private Quaternion startPointCameraQuat;

    [HideInInspector] public bool isOpened = false;
    public static bool isActive = false;
    private bool isSomebodyUse = false;

    private LocalizedString safe = new LocalizedString { TableReference = "safe" };

    private string ID { get; set; }

    void UpdateString(string translatedValue)
    {
        inputField.text = translatedValue;
    }

    void Awake()
    {
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
        safe.StringChanged += UpdateString;
        var parent = this.transform.parent;
        animator = parent.gameObject.GetComponentInChildren<Animator>();
        PV = GetComponent<PhotonView>();
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
    }

    void OnDestroy()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
        safe.StringChanged -= UpdateString;
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


    public void Active(PlayerController player)
    {
        if (isSomebodyUse || isOpened) return;

        if (!PhotonNetwork.OfflineMode)
            PV.RPC("SetUseable", RpcTarget.Others, true);
        
        cameraFromPlayer = player.cameraMain;
        startPointCameraPos = cameraFromPlayer.transform.position;
        startPointCameraQuat = cameraFromPlayer.transform.rotation;

        isActive = true;
        GameManager.isPaused = true;
        GameManager.instance.CursorView(true);
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
        PlayerData.instance.isItemActivated.Add(ID, isOpened);
    }

    public void Load()
    {
        isOpened = PlayerData.instance.isItemActivated[ID];
        animator.SetBool("Open", isOpened);
    }
}
