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
    }

    public void Active()
    {
        isMeow = true;
        animator.SetBool("Is Meow", isMeow);
        StartCoroutine(CDMeowRoutine());
    }

    private IEnumerator CDMeowRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        meowAudio.Play();
        yield return new WaitForSeconds(1.5f);
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
}
