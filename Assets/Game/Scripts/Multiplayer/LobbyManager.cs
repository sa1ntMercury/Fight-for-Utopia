using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header ("Connection with server")]
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private GameObject _connectToServerPanel;
    [SerializeField] private GameObject _multiplayerModePanel;

    [Header("Create/Join room")]
    [SerializeField] private TMP_InputField _createInput;
    [SerializeField] private TMP_InputField _joinInput;
    [SerializeField] private byte _maxPlayers;

    public void LobbyStart()
    {
        if (_usernameInput.text.Length >= 1)
        {
            _buttonText.text = "Connecting";
            PhotonNetwork.NickName = $"Player {_usernameInput}";
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        _buttonText.text = "Connect";
        DebugInfo.Instance.DebugMessage("Connected with server...");
        _connectToServerPanel.SetActive(false);
        _multiplayerModePanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public void MultiplayerLeft()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        DebugInfo.Instance.ClearDebug();
        DebugInfo.Instance.DebugMessage("Disconnected from server...");
    }

    public void CreateRoom()
    {
        if (_createInput.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(_createInput.text, new RoomOptions() { MaxPlayers = _maxPlayers });
            MultiplayerSettings._maxPlayers = _maxPlayers;
            _createInput.text = "";
        }
    }

    public override void OnCreatedRoom()
    {
        DebugInfo.Instance.ClearDebug();
        DebugInfo.Instance.DebugMessage("Room created...");
    }

    public void JoinRoom()
    {

        if (_joinInput.text.Length == 0)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.JoinRoom(_joinInput.text);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        DebugInfo.Instance.DebugMessage($"{_joinInput.text} does not exist");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        DebugInfo.Instance.DebugMessage($"There are any created rooms");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("CharacterSelection");

    }
}
 

    
