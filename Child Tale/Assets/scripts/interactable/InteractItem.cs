using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InteractItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Item item;
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Active()
    {
        if (InventoryManager.instance.Add(item))
        {
            if (PhotonNetwork.OfflineMode)
                DeleteTaked();
            else
                PV.RPC("DeleteTaked", RpcTarget.All);
        }
    }

    [PunRPC]
    void DeleteTaked()
    {
        Destroy(gameObject);
    }
}
