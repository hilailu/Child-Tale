using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.PunBasics;

public class CoopManager : MonoBehaviourPunCallbacks
{
    //public GameObject winnerUI;

    public GameObject player1SpawnPosition;
    public GameObject player2SpawnPosition;

    private GameObject player1;
    private GameObject player2;

    // Start Method

    private void Start()
    {
        if(!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
            return;
        }

        if(PlayerManager.LocalPlayerInstance == null)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                Debug.Log("instatntiating player 1");
                player1 = PhotonNetwork.Instantiate("player",
                    player1SpawnPosition.transform.position,
                    player1SpawnPosition.transform.rotation,0);
            }
            else
            {
                Debug.Log("instatntiating player 2");
                player2 = PhotonNetwork.Instantiate("player",
                    player2SpawnPosition.transform.position,
                    player2SpawnPosition.transform.rotation, 0);
            }
        }
    }

    // Update Method

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Application.Quit();
    //    }
    //}

    // Photon Methods

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("onplayerleftroom");
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(0);
        }
    }

    //Helper Methods

    //public void QuitRoom()
    //{
    //    Application.Quit();
    //}
}
