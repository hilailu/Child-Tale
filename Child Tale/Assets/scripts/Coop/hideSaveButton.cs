using UnityEngine;
using UnityEngine.UI;

public class hideSaveButton : MonoBehaviour
{
    private void Start()
    {
        if (!Photon.Pun.PhotonNetwork.OfflineMode)
            GetComponent<Button>().interactable = false;
    }
}
