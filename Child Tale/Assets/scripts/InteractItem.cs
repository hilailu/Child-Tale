using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Item item;

    public void Active()
    {
        if(InventoryManager.instance.Add(item))
        {
            Destroy(gameObject);
        }
    }
}
