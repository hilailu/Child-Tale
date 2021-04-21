using UnityEngine;
using Photon.Pun;

public class InteractItem : MonoBehaviour, IInteractable
{
    private string ID { get; set; }
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

    private void Update()
    {
        transform.Rotate(0, 1.5f, 0);
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
