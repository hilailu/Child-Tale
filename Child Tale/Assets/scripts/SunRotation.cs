using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    private float AngelX;

    private void Start()
    {
        transform.rotation = Quaternion.Euler( new Vector3(180 - (CustomTime.hours * 60 + CustomTime.minutes) * 360f / 24f / 60f, 0, 0));
    }

    void Update()
    {
        AngelX = 0.25f / CustomTime.OneCustomMinute * Time.deltaTime;
        transform.Rotate(-AngelX, 0, 0);
    }
}
