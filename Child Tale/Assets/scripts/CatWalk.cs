using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWalk : MonoBehaviour, IInteractable
{
    private float speed = 3f;
    [SerializeField] Animator animator;
    private AudioSource meowAudio;

    private bool isMeow = false;

    private void Start()
    {
        meowAudio = GetComponent<AudioSource>();
        StartCoroutine(MeowSomeTimesRoutine());
    }

    public void Active()
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


    void Update()
    {
        if (!isMeow)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 0.5f, out hit, 3f))
            {
                transform.Rotate(new Vector3(0, Random.Range(-100, 100), 0));
            }
        }
    }


    IEnumerator MeowSomeTimesRoutine()
    {
        yield return new WaitForSeconds(Random.Range(8f, 12f));
        if (!isMeow)
            meowAudio.Play();
        StartCoroutine(MeowSomeTimesRoutine());
    }
}
