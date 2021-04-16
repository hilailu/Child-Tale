using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private List<ISaveable> saves = new List<ISaveable>();

    private void Awake()
    {
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
        foreach (ISaveable item in ss)
        {
            saves.Add(item);
        }
        if (GameManager.isLoading == true)
        {           
            Load();
        }
    }

    public void Load()
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"), PlayerData.instance);
        foreach (var save in saves)
        {
            save.Load();
        }
    }

    public void Save()
    {
        foreach (var save in saves)
        {
            save.Save();
        }
        string player = JsonUtility.ToJson(PlayerData.instance, true);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", player);
    }

}
