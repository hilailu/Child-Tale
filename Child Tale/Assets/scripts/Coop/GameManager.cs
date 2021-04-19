using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.Video;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject player1SpawnPosition;
    public GameObject player2SpawnPosition;
    public GameObject singlePlayer;

    private GameObject player1;
    private GameObject player2;

    [SerializeField] private GameObject pause;
    [SerializeField] private VideoPlayer video;
    [SerializeField] private AudioListener listener;
    [SerializeField] private Animator _interactAnimator;


    public static bool isPaused;
    public static bool isLoading;

    [SerializeField] Animator endGameAnimator;
    [SerializeField] GameObject endGameCanvas;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion

    private void Start()
    {
        isPaused = false;

        if (PhotonNetwork.OfflineMode)
        {
            singlePlayer.SetActive(true);
            return;
        }

        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
            return;
        }

        // Создание игроков в мультиплеере
        if (PlayerManager.LocalPlayerInstance == null)
        {
            singlePlayer.SetActive(false);

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("instatntiating player 1");
                player1 = PhotonNetwork.Instantiate("player",
                    player1SpawnPosition.transform.position,
                    player1SpawnPosition.transform.rotation, 0);

                //player1.GetComponentInChildren<RayCastPlayer>().gameManager = this;

            }
            else
            {
                Debug.Log("instatntiating player 2");
                player2 = PhotonNetwork.Instantiate("player",
                    player2SpawnPosition.transform.position,
                    player2SpawnPosition.transform.rotation, 0);

                //player2.GetComponentInChildren<RayCastPlayer>().gameManager = this;

            }
        }
    }


    private void Update()
    {
        // Пауза
        if (!SafeCode.isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(!pause.activeSelf);
            if (!pause.activeSelf)
            {
                if (!TextFile.isFileOpen)
                    CursorView(true);

                CursorView(false);

                AudioListener.pause = false;
                Time.timeScale = 1f;
                isPaused = false;
            }
            else
            {
                CursorView(true);

                AudioListener.pause = true;
                isPaused = true;

                if (PhotonNetwork.OfflineMode)
                    Time.timeScale = 0f;
            }
        }
    }

    public void CursorView(bool view)
    {
        Cursor.visible = view;
        if (view)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }


    public void SetInteractableAnim(bool bol)
    {
        _interactAnimator.SetBool("InteractOpen", bol);
    }


    public void ToMenu()
    {
        if (PhotonNetwork.OfflineMode)
            SceneManager.LoadScene(0);
        else
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel(0);
            else
                PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("onplayerleftroom");

        if (otherPlayer.IsMasterClient)
            SceneManager.LoadScene(0);
    }


    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.Disconnect();
    }


    public void EndGame()
    {
        endGameCanvas.SetActive(true);
        endGameAnimator.SetBool("End Game", true);
        isPaused = true;
        StartCoroutine(LoadMenuRoutine());
    }

    System.Collections.IEnumerator LoadMenuRoutine()
    {
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene(0);
    }
}
