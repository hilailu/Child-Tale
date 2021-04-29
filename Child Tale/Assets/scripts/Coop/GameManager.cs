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

    public System.Action OnEndGame;
    public System.Action OnLoseGame;

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

        CursorView(false);

        // Создание игроков в мультиплеере
        if (PlayerManager.LocalPlayerInstance == null)
        {
            singlePlayer.SetActive(false);

            if (PhotonNetwork.IsMasterClient)
            {
                player1 = PhotonNetwork.Instantiate("player",
                    player1SpawnPosition.transform.position,
                    player1SpawnPosition.transform.rotation, 0);

            }
            else
            {
                player2 = PhotonNetwork.Instantiate("player",
                    player2SpawnPosition.transform.position,
                    player2SpawnPosition.transform.rotation, 0);
            }
        }
    }


    private void Update()
    {
        // Пауза
        if (Input.GetKeyDown(KeyCode.Escape) && !SafeCode.isActive && !EndGame.isGameEnd)
        {
            pause.SetActive(!pause.activeSelf);
            if (!pause.activeSelf)
            {
                if (TextFile.isFileOpen)
                    CursorView(true);
                else
                    CursorView(false);

                Time.timeScale = 1f;
                isPaused = false;
                AudioListener.pause = false;
            }
            else
            {
                CursorView(true);
                isPaused = true;

                if (PhotonNetwork.OfflineMode)
                {
                    AudioListener.pause = true;
                    Time.timeScale = 0f;
                }
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
        => _interactAnimator.SetBool("InteractOpen", bol);


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
        if (otherPlayer.IsMasterClient)
            SceneManager.LoadScene(0);
    }


    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.Disconnect();
    }
}
