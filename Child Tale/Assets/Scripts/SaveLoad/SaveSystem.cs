using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static Action onSave;
    public static Action onLoad;

    public static void Load()
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"), PlayerData.instance);
        onLoad?.Invoke();
    }

    public static void Save()
    {
        PlayerData.instance.isItemActivated.Clear();
        onSave?.Invoke();
        string player = JsonUtility.ToJson(PlayerData.instance, true);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", player);
    }

}
