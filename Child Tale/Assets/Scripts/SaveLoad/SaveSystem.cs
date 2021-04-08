using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private List<ISaveable> saves = new List<ISaveable>();
    private List<GameObject> savesGO;

    private void Awake()
    {
        foreach (var item in savesGO)
        {
            saves.Add(item.GetComponent<ISaveable>());
        }
    }
    public void Load()
    {
        print("load");
        foreach (var save in saves)
        {
            save.Load();
        }
    }

    public void Save()
    {
        print("save");
        foreach (var save in saves)
        {
            save.Save();
        }
    }

    public void DeleteSave()
    {
        print("delete save");
        foreach (var save in saves)
        {
            save.DeleteSave();
        }
    }

}
