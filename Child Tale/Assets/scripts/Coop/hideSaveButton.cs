using System.Collections;
using System.Collections.Generic;
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
