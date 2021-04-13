using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour, ISaveable
{
    #region Singleton
    public static InventoryManager instance;
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();
    private int storage = 5;
    public InventoryItem[] inventoryItems;

    public Action OnInventoryChanged;

    void Start()
    {
        instance.OnInventoryChanged += UpdateUI;
        inventoryItems = GetComponentsInChildren<InventoryItem>(true);
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (i < items.Count)
            {
                inventoryItems[i].Add(items[i]);
            }
            else
            {
                inventoryItems[i]?.Remove();
            }
        }
    }

    public bool Add(Item item)
    {
        if (items.Count >= storage)
            return false;
        if (!items.Contains(item))
            items.Add(item);
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        OnInventoryChanged?.Invoke();
    }

    public void Save()
    {
        PlayerData.instance.items = new List<Item>(this.items);
        Debug.Log("Save Inventory");
        string player = JsonUtility.ToJson(PlayerData.instance, true);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", player);
    }

    public void Load()
    {
        Debug.Log("Load Inventory");
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"), PlayerData.instance);
        this.items = new List<Item>(PlayerData.instance.items);
        OnInventoryChanged?.Invoke();
    }
}
