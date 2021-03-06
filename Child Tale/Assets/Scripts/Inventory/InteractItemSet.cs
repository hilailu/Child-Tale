using System.Collections.Generic;
using UnityEngine;

public class InteractItemSet : MonoBehaviour, ISaveable
{
    public static HashSet<string> CollectedItems { get; private set; } = new HashSet<string>();

    private void Awake()
    {
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
    }

    void OnDestroy()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }

    public void Load()
        => CollectedItems = new HashSet<string>(PlayerData.instance.CollectedItems);

    public void Save()
    {
        PlayerData.instance.CollectedItems = new string[CollectedItems.Count];
        CollectedItems.CopyTo(PlayerData.instance.CollectedItems);
    }
}
