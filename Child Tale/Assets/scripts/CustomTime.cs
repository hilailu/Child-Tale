using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTime : MonoBehaviour
{
    public static float hours = 9;
    public static float minutes = 0;
    public static float OneCustomMinute = 1f;


    private void Start()
    {
        StartCoroutine(TimeRoutine());
    }

    private IEnumerator TimeRoutine()
    {
        minutes++;
        if (minutes > 59)
        {
            hours++;
            minutes = 0;
        }
        if (hours > 23)
            hours = 0;

        yield return new WaitForSeconds(OneCustomMinute);
        StartCoroutine(TimeRoutine());
    }
}
