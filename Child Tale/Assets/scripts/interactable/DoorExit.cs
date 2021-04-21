using UnityEngine;
using Photon.Pun;

public class DoorExit : MonoBehaviour, IInteractable
{
    [SerializeField] InventoryManager inventory;
    [SerializeField] AudioClip knobSounde;
    [SerializeField] AudioClip openDoorSounde;
    [SerializeField] Animator animator;
    [SerializeField] Item key;
    private AudioSource audioSource;
    private PhotonView PV;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
    }

    public void Active()
    {
        if (inventory.items.Contains(key))
        {
            if (PhotonNetwork.OfflineMode)
                GameOver();
            else
                PV.RPC("GameOver", RpcTarget.All);
        }
        else
        {
            if (PhotonNetwork.OfflineMode)
                KnobAnim();
            else
                PV.RPC("KnobAnim", RpcTarget.All);
        }
    }

    [PunRPC]
    void KnobAnim()
    {
        animator.SetTrigger("Knob");
        audioSource.PlayOneShot(knobSounde);
    }

    [PunRPC]
    void GameOver()
    {
        animator.SetTrigger("Game Over");
        audioSource.PlayOneShot(openDoorSounde);
        GameManager.instance.OnEndGame?.Invoke();
    }
}
