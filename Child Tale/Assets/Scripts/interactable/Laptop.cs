using UnityEngine;
using Photon.Pun;

public class Laptop : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject laptopUI;
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Active()
    {
        if (PhotonNetwork.OfflineMode)
            Show();
        else
            PV.RPC("Show", RpcTarget.All);
    }

    [PunRPC]
    void Show()
    {
        laptopUI.SetActive(!laptopUI.activeSelf);
    }
}
