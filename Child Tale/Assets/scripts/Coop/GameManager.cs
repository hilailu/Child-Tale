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


    // Start Method

    private void Start()
    {
        isPaused = false;
        AudioListener.pause = false;

        if (PhotonNetwork.OfflineMode) return;

        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
            return;
        }

        if (PlayerManager.LocalPlayerInstance == null)
        {
            Destroy(singlePlayer);

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("instatntiating player 1");
                player1 = PhotonNetwork.Instantiate("player",
                    player1SpawnPosition.transform.position,
                    player1SpawnPosition.transform.rotation, 0);
                //player1.gameObject.name = player1.GetComponent<PhotonView>().Owner.NickName;

                player1.GetComponentInChildren<RayCastPlayer>().gameManager = this;

            }
            else
            {
                Debug.Log("instatntiating player 2");
                player2 = PhotonNetwork.Instantiate("player",
                    player2SpawnPosition.transform.position,
                    player2SpawnPosition.transform.rotation, 0);
                //player2.gameObject.name = player1.GetComponent<PhotonView>().Owner.NickName;

                player2.GetComponentInChildren<RayCastPlayer>().gameManager = this;

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
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (video.isPaused)
                    video.Play();
                AudioListener.pause = false;
                Time.timeScale = 1f;
                isPaused = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (video.isPlaying)
                    video.Pause();
                AudioListener.pause = true;
                isPaused = true;
                if (PhotonNetwork.OfflineMode)
                    Time.timeScale = 0f;
            }
        }
    }


    public void SetInteractableAnim(bool bol)
    {
        _interactAnimator.SetBool("InteractOpen", bol);
    }


    public void ToMenu()
    {
        PhotonNetwork.LoadLevel(0);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("onplayerleftroom");
        if (otherPlayer.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(0);
        }
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
