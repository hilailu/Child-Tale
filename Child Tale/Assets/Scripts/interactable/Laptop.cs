using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject laptopUI;

    public void Active()
    {
        laptopUI.SetActive(!laptopUI.activeSelf);
    }

}
