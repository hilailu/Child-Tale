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
            {
                GameOver();
                EndGameAnim();
            }
            else
            {
                PV.RPC("GameOver", RpcTarget.All);
                PV.RPC("EndGameAnim", RpcTarget.All);
            }
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
        GameManager.isPaused = true;
        audioSource.PlayOneShot(openDoorSounde);
    }

    [PunRPC]
    void EndGameAnim()
        => GameManager.instance.EndGame();
}
