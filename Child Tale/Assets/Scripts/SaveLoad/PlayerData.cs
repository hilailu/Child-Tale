using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour, ISerializationCallbackReceiver
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
    public bool isHungry;
    public float minutes;
    public float hours;
    public float microwave;
    public string[] CollectedItems;
    public int fear;
    public int messages;

    public List<string> _keys = new List<string>();
    public List<bool> _values = new List<bool>();
    public Dictionary<string, bool> isItemActivated = new Dictionary<string, bool>();

    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in isItemActivated)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        isItemActivated = new Dictionary<string, bool>();

        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            isItemActivated.Add(_keys[i], _values[i]);
    }
}
