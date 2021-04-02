using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waving : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void Start()
    {
        StartCoroutine("WavingRoutine");
    }

    private IEnumerator WavingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);
            anim.SetTrigger("Wave");
        }
    }
}
