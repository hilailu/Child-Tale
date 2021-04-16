using Photon.Pun;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour, IInteractable, ISaveable
{
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] AudioClip openDoor;
    [SerializeField] AudioClip closeDoor;
    private bool isOpen;

    private PhotonView photonView;

    private string ID { get; set; }

    private void Awake()
    {
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>(); 
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
    }

    void OnDestroy()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }

    public void Active()
    {
        isOpen = !isOpen;
        if (PhotonNetwork.OfflineMode)
            playAnim(isOpen);
        else
            photonView.RPC("playAnim", RpcTarget.All, isOpen);
    }


    [PunRPC]
    void playAnim(bool bol)
    {
        animator.SetBool("Is Open", bol);
        if (bol)
            audioSource.PlayOneShot(openDoor);
        else
            audioSource.PlayOneShot(closeDoor);
    }

    public void Save()
    {
        PlayerData.instance.isItemActivated.Add(ID, isOpen);
    }

    public void Load()
    {
        isOpen = PlayerData.instance.isItemActivated[ID];
        animator.SetBool("Is Open", isOpen);
    }
}
