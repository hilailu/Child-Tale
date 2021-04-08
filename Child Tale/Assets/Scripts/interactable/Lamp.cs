using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
    private Light svet;

    void Start()
    {
        svet = GetComponentInChildren<Light>();
    }

    public void Active()
    {
        svet.enabled = !svet.isActiveAndEnabled;
    }

}
