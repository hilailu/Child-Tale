using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWalk : MonoBehaviour, IInteractable
{
    private float speed = 3f;
    [SerializeField] Animator animator;

    private bool isMeow = false;

    public void Active()
    {
        isMeow = true;
        animator.SetBool("Is Meow", isMeow);
        StartCoroutine(CDMeowRoutine());
    }

    private IEnumerator CDMeowRoutine()
    {
        yield return new WaitForSeconds(3f);
        isMeow = false;
        animator.SetBool("Is Meow", isMeow);
    }


    // Update is called once per frame
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
