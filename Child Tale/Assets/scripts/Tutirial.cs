using UnityEngine;

public class Tutirial : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        if (Photon.Pun.PhotonNetwork.OfflineMode)
        {
            transform.gameObject.SetActive(true);
            GameManager.instance.CursorView(true);
            GameManager.isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            transform.gameObject.SetActive(false);
            audioSource.Play();
        }
    }

    public void CloseButton()
    {
        transform.gameObject.SetActive(false);
        CancelTutorial();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.gameObject.SetActive(false);
            CancelTutorial();
        }
    }

    void CancelTutorial()
    {
        GameManager.instance.CursorView(false);
        GameManager.isPaused = false;
        Time.timeScale = 1f;
        audioSource.Play();
    }
}
