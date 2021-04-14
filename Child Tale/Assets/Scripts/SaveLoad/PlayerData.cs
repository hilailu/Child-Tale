using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    #region Singleton
    public static PlayerData instance;
    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();
    public Vector3 pos;
    public Quaternion rot;
    public bool isSafeOpened;
    public float minutes;
    public float hours;

}
