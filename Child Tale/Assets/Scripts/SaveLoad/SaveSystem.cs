using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private List<ISaveable> saves = new List<ISaveable>();
    [SerializeField] private List<GameObject> savesGO;

    private void Awake()
    {
        foreach (var item in savesGO)
        {
            saves.Add(item.GetComponent<ISaveable>());
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
