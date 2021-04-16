using UnityEngine;
using Photon.Pun;

public class Lamp : MonoBehaviour, IInteractable, ISaveable
{
    private Light svet;
    private PhotonView PV;

    private string ID { get; set; }

    private void Awake()
    {
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
        svet = GetComponentInChildren<Light>();
        PV = GetComponent<PhotonView>();
    }

    void OnDestroy()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
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

    public void Save()
    {
        PlayerData.instance.isItemActivated.Add(ID, svet.isActiveAndEnabled);
    }

    public void Load()
    {
        svet.enabled = PlayerData.instance.isItemActivated[ID];
    }
}
