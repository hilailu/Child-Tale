using UnityEngine;
using UnityEngine.UI;

public class hideSaveButton : MonoBehaviour
{
    // отдельный скрипт, чтоб не захламлять game manager
    private void Start()
    {
        if (!Photon.Pun.PhotonNetwork.OfflineMode)
            GetComponent<Button>().interactable = false;
    }
}
