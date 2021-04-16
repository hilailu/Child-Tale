using UnityEngine;
using Photon.Pun;

public class Lamp : MonoBehaviour, IInteractable
{
    private Light svet;
    private PhotonView PV;

    void Start()
    {
        svet = GetComponentInChildren<Light>();
        PV = GetComponent<PhotonView>();
    }

    public void Active()
    {
        if (PhotonNetwork.OfflineMode)
            SetLight();
        else
            PV.RPC("SetLight", RpcTarget.All);

    }

    [PunRPC]
    void SetLight()
    {
        svet.enabled = !svet.isActiveAndEnabled;
    }
}
