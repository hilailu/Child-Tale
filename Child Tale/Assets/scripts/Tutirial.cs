using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutirial : MonoBehaviour
{
    private void Start()
    {
        if (Photon.Pun.PhotonNetwork.OfflineMode)
        {
            print("tutorial");
            transform.gameObject.SetActive(true);
            GameManager.instance.CursorView(true);
        }
        else
            transform.gameObject.SetActive(false);
    }

    public void CloseButton()
    {
        transform.gameObject.SetActive(false);
        GameManager.instance.CursorView(false);
    }
}
