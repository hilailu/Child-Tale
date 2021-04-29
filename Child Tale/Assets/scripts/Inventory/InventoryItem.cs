using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{

    [SerializeField] private Image _icon;

    public Item _item;

    public void Add(Item item)
    {
        _item = item;
        _icon.sprite = item.icon;
        _icon.enabled = true;
    }

    public void Remove()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }
}
