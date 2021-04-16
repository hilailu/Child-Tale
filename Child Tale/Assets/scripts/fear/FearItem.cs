using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FearItem : MonoBehaviour, IPlayerInteractive
{
    [SerializeField] int fearAmount;

    public void Active(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        transform.Rotate(0, 2, 0);
    }
}
