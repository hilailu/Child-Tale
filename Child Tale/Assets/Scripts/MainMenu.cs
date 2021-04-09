using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] Launcher launcher;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        Photon.Pun.PhotonNetwork.OfflineMode = true;
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

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ConnectToPhotonServer()
        => launcher.ConnectToPhoton();
}
