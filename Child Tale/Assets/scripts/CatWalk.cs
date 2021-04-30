using System.Collections;
using UnityEngine;
using Photon.Pun;

public class CatWalk : MonoBehaviour, IInteractable
{
    [SerializeField] Animator animator;
    private AudioSource meowAudio;
    [SerializeField] private GameObject paperCode;
    [SerializeField] Item catFood;
    private PhotonView PV;

    private float speed = 3f;
    private bool isMeow = false;

    private void Start()
    {
        meowAudio = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
        StartCoroutine(MeowSomeTimesRoutine());
    }


    public void Active()
    {
        if (PhotonNetwork.OfflineMode)
            PlayMeowAnim();
        else
            PV.RPC("PlayMeowAnim", RpcTarget.All);
        FeedCat();
    }


    [PunRPC]
    void PlayMeowAnim()
    {
        isMeow = true;
        animator.SetBool("Is Meow", isMeow);
        meowAudio.PlayDelayed(0.5f);
        StartCoroutine(CDMeowRoutine());
    }


    private IEnumerator CDMeowRoutine()
    {
        yield return new WaitForSeconds(2f);
        isMeow = false;
        animator.SetBool("Is Meow", isMeow);
    }


    private void FeedCat()
    {
        // Спаун бумажки с кодом, если есть корм в кармане
        if (InventoryManager.instance.items.Contains(catFood))
        {
            Vector3 spawnPos = transform.localPosition + transform.forward + transform.up;

            GameObject paper;
            if (PhotonNetwork.OfflineMode)
                paper = Instantiate(paperCode, spawnPos, Quaternion.identity);
            else
                paper = PhotonNetwork.Instantiate("Paper", spawnPos, Quaternion.identity, 0);

            StartCoroutine(IncreaseSizeRoutine(paper));
            InventoryManager.instance.Remove(catFood);
        }
    }


    // Постепенное увеличение в размерах)))
    private IEnumerator IncreaseSizeRoutine(GameObject obj)
    {
        Vector3 scaleFactor = obj.transform.localScale /= 20;
        for (int i = 0; i < 20; i++)
        {
            if (obj == null) break;
            obj.transform.localScale += scaleFactor;
            yield return new WaitForSeconds(0.05f);
        }
    }


    // Умный кот
    void Update()
    {
        if (!isMeow)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 1f, out hit, 3f))
            {
                transform.Rotate(new Vector3(0, Random.Range(-100, 100), 0));
            }
        }
    }


    // Чтоб мяукал иногда
    IEnumerator MeowSomeTimesRoutine()
    {
        yield return new WaitForSeconds(Random.Range(8f, 12f));

        if (!isMeow)
            meowAudio.Play();

        StartCoroutine(MeowSomeTimesRoutine());
    }
}
