using UnityEngine;
using TMPro;
using Photon.Pun;

public class Phone : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject phoneUI;
    [SerializeField] private TMP_Text time;
    private Animator anim;
    public bool phone;

    [HideInInspector] public PhotonView PV;

    void Start()
    {
        phone = false;
        anim = GetComponent<Animator>();
        PV = GetComponentInParent<PhotonView>();
    }

    void Update()
    {
        time.text = $"{CustomTime.hours:00}:{CustomTime.minutes:00}";

        if (!GameManager.isPaused && PV.IsMine && !TextFile.isFileOpen)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                phone = !phone;

                if (!phone)
                {
                    GameManager.instance.CursorView(false);
                    phoneUI.SetActive(false);
                }
                else
                {
                    player.cameraMain.transform.localRotation = Quaternion.identity;
                    GameManager.instance.CursorView(true);

                }
            }

            if (Input.GetKeyDown(KeyCode.U) && phone)
            {
                phoneUI.SetActive(!phoneUI.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                phone = false;
                phoneUI.SetActive(false);
            }
        }

        // Открытие смартфона
        anim.SetBool("PhoneOpen", phone);
        if (!phone)
            Invoke("Hide", 0.5f);
        else
            Hide();
    }


    void Hide()
        => GetComponent<MeshRenderer>().enabled = phone;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(phone);
        }
        else if (stream.IsReading)
        {
            phone = (bool)stream.ReceiveNext();
        }
    }
}
