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

  

    private void Start()
        => buttonLoadArena.SetActive(false);

    private void Awake()
        => PhotonNetwork.AutomaticallySyncScene = true;

    public void SetPlayerName(string name)
        => playerName = name;

    public void SetRoomName(string name)
        => roomName = name;

    public void Disconect()
    {
        PhotonNetwork.Disconnect();

        ActiveLoadButton(false);
        connectionStatus.text = string.Empty;
    }

    public void ConnectToPhoton()
    {
        connectionStatus.text = "connecting...";
        connectionStatus.color = Color.white;
        PhotonNetwork.GameVersion = gameVersion;

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
            PhotonNetwork.LoadLevel(1);
        else
            connectionStatus.text = "Need minimum 2 players connected";
    }

    private void ActiveLoadButton(bool avtive)
    {
        buttonLoadArena.SetActive(avtive);
        buttonJoinRoom.SetActive(!avtive);
        buttonCreateRoom.SetActive(!avtive);
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
        => PhotonNetwork.OfflineMode = true;


    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
            ActiveLoadButton(true);
    }
}
