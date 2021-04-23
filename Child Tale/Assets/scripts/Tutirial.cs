using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutirial : MonoBehaviour
{
    private void Start()
    {
        if (Photon.Pun.PhotonNetwork.OfflineMode)
        {
            transform.gameObject.SetActive(true);
            GameManager.instance.CursorView(true);
            GameManager.isPaused = true;
            Time.timeScale = 0f;
        }
        else
            transform.gameObject.SetActive(false);
    }

    public void CloseButton()
    {
        transform.gameObject.SetActive(false);
        GameManager.instance.CursorView(false);
        GameManager.isPaused = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            transform.gameObject.SetActive(false);
    }
}
