using UnityEngine;
using Photon.Pun;
using System.IO;

public class food : MonoBehaviour, IPlayerInteractive
{
    private PhotonView PV;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json") && !PlayerData.instance.isHungry)
            Destroy(this.gameObject);
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
