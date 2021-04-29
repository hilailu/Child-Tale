using UnityEngine;
using Photon.Pun;
using System.IO;

public class InteractItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Item item;
    private PhotonView PV;

    private string ID { get; set; }

    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            if (InteractItemSet.CollectedItems.Contains(ID))
            {
                Destroy(this.gameObject);
                return;
            }
        }
        PV = GetComponent<PhotonView>();
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();   
    }

    private void Update()
        => transform.Rotate(0, 1.5f, 0);

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
        => Destroy(gameObject);
}
