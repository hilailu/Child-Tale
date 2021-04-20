using System.Collections;
using UnityEngine;

public class CustomTime : MonoBehaviour, ISaveable
{
    public static float hours;
    public static float minutes;
    public static float OneCustomMinute = 1f;

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
        if (hours == 17)
            GameManager.instance.OnEndGame?.Invoke();

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

    public static string ToStringTime()
        => $"{hours:00}:{minutes:00}";

    public void Save()
    {
        PlayerData.instance.hours = hours;
        PlayerData.instance.minutes = minutes;
    }

    public void Load()
    {
        minutes = PlayerData.instance.minutes;
        hours = PlayerData.instance.hours;
    }
}
