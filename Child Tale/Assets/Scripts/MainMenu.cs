using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] Launcher launcher;
    [SerializeField] private Button newGame;
    [SerializeField] private Button continueGame;

    private void Awake()
    {
        GameManager.isLoading = false;
        GameManager.isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CheckSave();
    }

    public void CheckSave()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            newGame.gameObject.SetActive(false);
            continueGame.gameObject.SetActive(true);
        }
        else
        {
            newGame.gameObject.SetActive(true);
            continueGame.gameObject.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        Photon.Pun.PhotonNetwork.OfflineMode = true;
        GameManager.isLoading = false;
    }

    public void Load()
    {
        SceneManager.LoadScene(1);
        Photon.Pun.PhotonNetwork.OfflineMode = true;
        GameManager.isLoading = true;
    }

    public void DeleteProgress()
    {
        File.Delete(Application.persistentDataPath + "/PlayerData.json");
        CheckSave();
    }

    public void Volume(float vol)
    {
        mixer.SetFloat("volume", vol);
    }

    public void Language()
    {
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        else LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ConnectToPhotonServer()
        => launcher.ConnectToPhoton();
}
