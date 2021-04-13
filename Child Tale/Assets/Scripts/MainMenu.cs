using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] Launcher launcher;
    [SerializeField] private Button newGame;
    [SerializeField] private Button continueGame;
    [SerializeField] private TMP_Text newOrContinue;
    private LocalizedString newOrNot = new LocalizedString { TableReference = "UI Text", TableEntryReference = "New"};

    void UpdateString(string translatedValue)
    {
        newOrContinue.text = translatedValue;
    }

    private void Awake()
    {
        GameManager.isLoading = false;
        GameManager.isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        newOrNot.StringChanged += UpdateString;
    }

    private void Start()
    {
        CheckSave();
    }

    public void CheckSave()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
            newOrNot.TableEntryReference = "Continue";
        else
            newOrNot.TableEntryReference = "New";
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        Photon.Pun.PhotonNetwork.OfflineMode = true;
        if (newOrNot.TableEntryReference == "Continue")
            GameManager.isLoading = true;
        else
            GameManager.isLoading = false;
    }

    public void DeleteProgress()
    {
        File.Delete(Application.persistentDataPath + "/PlayerData.json");
        newOrNot.TableEntryReference = "New";
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
