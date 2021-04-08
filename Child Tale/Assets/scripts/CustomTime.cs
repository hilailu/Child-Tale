using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTime : MonoBehaviour, ISaveable
{
    [SerializeField] private float h, m;
    public static float hours = 9;
    public static float minutes = 0;
    public static float OneCustomMinute = 1f;

    private void Start()
    {
        StartCoroutine(TimeRoutine());
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(-(hours * 60 + minutes) * 0.5f - 90, 0, 0);
        h = hours;
        m = minutes;
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
        Debug.Log("Save Time");
        PlayerPrefs.SetString("TimeJSON", JsonUtility.ToJson(this, true));
        PlayerPrefs.Save();
    }

    public void Load()
    {
        Debug.Log("Load Time");
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("TimeJSON"), this);
        minutes = m;
        hours = h;
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("TimeJSON");
    }
}
