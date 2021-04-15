using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    string gameVersion = "1";
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
    {
        PhotonNetwork.Disconnect();

        buttonLoadArena.SetActive(false);
        buttonJoinRoom.SetActive(true);
        buttonCreateRoom.SetActive(true);
        connectionStatus.text = string.Empty;
    }

    public void ConnectToPhoton()
    {
        connectionStatus.text = "connecting...";
        PhotonNetwork.GameVersion = gameVersion;
        connectionStatus.color = Color.white;

        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
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
            connectionStatus.text = $"You was join to room \"{roomName}\"";
        }
        else
            connectionStatus.text = "You are not connected";
    }

    public void LoadArena()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
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
        PhotonNetwork.OfflineMode = false;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("disconnected");
        PhotonNetwork.OfflineMode = true;
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            buttonLoadArena.SetActive(true);
            buttonJoinRoom.SetActive(false);
            buttonCreateRoom.SetActive(false);
        }
    }
}
