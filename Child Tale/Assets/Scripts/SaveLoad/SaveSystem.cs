using System.Collections;
using System.Collections.Generic;
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

}
