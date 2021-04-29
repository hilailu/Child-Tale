using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{

    private void Awake()
        => SceneManager.sceneLoaded += OnSceneLoaded;

    private void OnDestroy()
        => SceneManager.sceneLoaded -= OnSceneLoaded;

    public void Save()
        => SaveSystem.Save();

    static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.isLoading == true)
            SaveSystem.Load();
    }
}
