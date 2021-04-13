using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomTime : MonoBehaviour, ISaveable
{
    public static float hours;
    public static float minutes;
    public static float OneCustomMinute = 1f;

    private void Start()
    {
        if (!GameManager.isLoading)
        {
            hours = 9;
            minutes = 0;
        }
        StartCoroutine(TimeRoutine());
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(-(hours * 60 + minutes) * 0.5f - 90, 0, 0);
    }

    private IEnumerator TimeRoutine()
    {
        minutes++;
        if (minutes > 59)
        {
            hours++;
            minutes = 0;
        }
        if (hours > 23)
            hours = 0;

        yield return new WaitForSeconds(OneCustomMinute);
        StartCoroutine(TimeRoutine());
    }

    public void Save()
    { 
        PlayerData.instance.hours = hours;
        PlayerData.instance.minutes = minutes;
        Debug.Log("Save Time");
        string player = JsonUtility.ToJson(PlayerData.instance, true);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", player);
    }

    public void Load()
    {
        Debug.Log("Load Time");
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"), PlayerData.instance);
        minutes = PlayerData.instance.minutes;
        hours = PlayerData.instance.hours;
    }
}
