using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Microwave : MonoBehaviour, IInteractable, ISaveable
{
    private AudioSource audioSource;
    private Animator animator;
    private PhotonView PV;
    [SerializeField] AudioClip openSounde;
    [SerializeField] AudioClip workingSounde;
    private bool isWorking;
    private float timeLeft;

    private string ID { get; set; }

    private void Awake()
    {
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
        audioSource = GetComponentInParent<AudioSource>();
        animator = GetComponentInParent<Animator>();
        PV = GetComponent<PhotonView>();
        isWorking = false;
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
    }

    private void OnDestroy()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }

    public void Active()
    {
        if (isWorking) return;

        if (PhotonNetwork.OfflineMode)
            playAnim();
        else
            PV.RPC("playAnim", RpcTarget.All);
    }

    [PunRPC]
    void playAnim()
        => StartCoroutine(WorkingMicrowaveRoutine(60f));

    // Имитация работы микроволновки
    IEnumerator WorkingMicrowaveRoutine(float time)
    {
        isWorking = true;
        audioSource.PlayOneShot(workingSounde);
        for (float i = 1; i <= time; i++)
        {
            yield return new WaitForSecondsRealtime(1f);
            timeLeft = time - i;
        }
        animator.SetBool("IsOpen", isWorking);
        audioSource.PlayOneShot(openSounde);
    }

    public void Save()
    {
        PlayerData.instance.microwave = this.timeLeft;
        PlayerData.instance.isItemActivated.Add(ID, isWorking);
    }

    public void Load()
    {
        this.timeLeft = PlayerData.instance.microwave;
        isWorking = PlayerData.instance.isItemActivated[ID];
        if (timeLeft > 0f)
            StartCoroutine(WorkingMicrowaveRoutine(timeLeft));
        else
            animator.SetBool("IsOpen", isWorking);
    }
}
