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

    public List<InteractItem> itemsInScene = new List<InteractItem>();

    void Start()
    {
        // Лист интерактивных предметов в сцене
        InteractItem[] items = (InteractItem[])FindObjectsOfType(typeof(InteractItem));
        itemsInScene = new List<InteractItem>(items);

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
    }

    public void Load()
    {
        this.items = new List<Item>(PlayerData.instance.items);
        OnInventoryChanged?.Invoke();
    }
}
