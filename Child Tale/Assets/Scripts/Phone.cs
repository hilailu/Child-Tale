using UnityEngine;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class Phone : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private PlayerController player;
    //[SerializeField] private Transform endMarker;
    [SerializeField] private GameObject phoneUI;
    [SerializeField] private TMP_Text time;
    private Animator anim;
    public  bool phone;

    public PhotonView PV;

    void Start()
    {
        anim = GetComponent<Animator>();
        PV = GetComponentInParent<PhotonView>();
    }

    void Update()
    {
        time.text = $"{CustomTime.hours:00}:{CustomTime.minutes:00}";

        if (!GameManager.isPaused && !TextFile.isFileOpen && PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                phone = !phone;
                //anim.SetBool("PhoneOpen", phone);


                //if (PhotonNetwork.OfflineMode)
                //    anim.SetBool("PhoneOpen", phone);
                //else
                //    PV.RPC("PhoneAnim", RpcTarget.All, phone);


                //if(PV.IsMine)
                //{
                //    Hashtable hash = new Hashtable();
                //    hash.Add("phone", phone);
                //    PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
                //}


                if (!phone)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    phoneUI.SetActive(false);
                    //Invoke("Hide", 0.5f);
                }
                else
                {
                    player.cameraMain.transform.localRotation = Quaternion.identity;
                    //Hide();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
            if (phone && Input.GetKeyDown(KeyCode.U))
            {
                phoneUI.SetActive(!phoneUI.activeSelf);
            }
        }

        anim.SetBool("PhoneOpen", phone);
        if (!phone)
            Invoke("Hide", 0.5f);
        else
            Hide();
    }

    void Hide()
    {
        GetComponent<MeshRenderer>().enabled = phone;
    }

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


    //[PunRPC]
    //private void PhoneAnim(bool choise)
    //{
    //    if (!PV.IsMine)
    //        anim.SetBool("PhoneOpen", choise);
    //}


    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    //{
    //    if (!PV.IsMine && targetPlayer == PV.Owner)
    //    {
    //        anim.SetBool("PhoneOpen", (bool)changedProps["phone"]);
    //    }
    //}
}
