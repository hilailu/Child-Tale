using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class food : MonoBehaviour, IPlayerInteractive
{
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Active(PlayerController player)
    {
        player.isHungry = false;

        if (PhotonNetwork.OfflineMode)
            Eat();
        else
            PV.RPC("Eat", RpcTarget.All);
    }

    [PunRPC]
    void Eat()
        => Destroy(gameObject);
}
