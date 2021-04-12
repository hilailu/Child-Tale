using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    //[SerializeField]
    //private GameObject controlPanel;

    //[SerializeField]
    //private Text feedbackText;

    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    //bool isConnecting;

    string gameVersion = "1";

    //[Space(10)]
    //[Header("Custom Variables")]
    //public InputField playerNameField;
    //public InputField roomNameField;

    //[Space(5)]
    //public Text playerStatus;
    public Text connectionStatus;

    [Space(5)]
    public GameObject buttonLoadArena;
    public GameObject buttonCreateRoom;
    public GameObject buttonJoinRoom;

    string playerName = string.Empty;
    string roomName = string.Empty;

    // Start Method

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        buttonLoadArena.SetActive(false);

        //ConnectToPhoton();
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Helper Methods

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    }

    // Tutorial Methods

    public void Disconect()
        => PhotonNetwork.Disconnect();

    public void ConnectToPhoton()
    {
        connectionStatus.text = "connecting...";
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxPlayersPerRoom;
            TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default);
            PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby);
        }
    }

    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public void LoadArena()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 0)
        {
            PhotonNetwork.LoadLevel(1);
        }
        else
            connectionStatus.text = "Need minimum 2 players connected";
    }

    // Photon Methods

    public override void OnConnected()
    {
        base.OnConnected();
        connectionStatus.text = "connected to server";
        connectionStatus.color = Color.yellow;
        buttonLoadArena.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //isConnecting = false;
        //controlPanel.SetActive(true);
        Debug.Log("disconnected");
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            buttonLoadArena.SetActive(true);
            buttonJoinRoom.SetActive(false);
            buttonCreateRoom.SetActive(false);
            //playerStatus.text = "you are lobby leader";
        }
        else
        {
            //playerStatus.text = "connected to lobby";
        }
    }

}
