using System;
using System.Collections;
using System.Collections.Generic;
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
                inventoryItems[i].Remove();
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
        Debug.Log("Save Inventory");
        PlayerPrefs.SetString("InventoryJSON", JsonUtility.ToJson(this, true));
        PlayerPrefs.Save();
    }

    public void Load()
    {
        Debug.Log("Load Inventory");
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("InventoryJSON"), this);
        OnInventoryChanged?.Invoke();
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("InventoryJSON");
    }
}
