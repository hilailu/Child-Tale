using UnityEngine;
using UnityEngine.UI;

public class hideSaveButton : MonoBehaviour
{
    // ��������� ������, ���� �� ���������� game manager
    private void Start()
    {
        if (!Photon.Pun.PhotonNetwork.OfflineMode)
            GetComponent<Button>().interactable = false;
    }
}
