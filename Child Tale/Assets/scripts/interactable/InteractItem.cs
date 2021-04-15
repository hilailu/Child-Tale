using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class InteractItem : MonoBehaviour, IInteractable
{
    public string ID { get; private set; }
    [SerializeField] private Item item;
    private PhotonView PV;

    private void Awake()
    {
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();   
    }

    private void Start()
    {
        if (InteractItemSet.CollectedItems.Contains(ID))
        {
            Destroy(this.gameObject);
            return;
        }
        PV = GetComponent<PhotonView>();
     
    }

    public void Active()
    {
        if (InventoryManager.instance.Add(item))
        {
            InteractItemSet.CollectedItems.Add(ID);
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
