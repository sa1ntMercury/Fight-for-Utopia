using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<Transform> _spawnPoints; // Points for spawn

    private string _playerName; // Name of prefab in Resources folder

    private void Start()
    {
        // Get spawn point
        int point = Random.Range(0, _spawnPoints.Count);

        Vector3 playerPosition = _spawnPoints[point].position;
        _spawnPoints.RemoveAt(point);

        //Vector3 playerPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;

        // Create player object

        _playerName = "Multiplayer" + MultiplayerSettings._prefabName;
        PhotonNetwork.Instantiate(_playerName, playerPosition, Quaternion.identity);
    }



    public void ExitMultiplayer()
    {
        SceneManager.LoadScene("MainMenu");
        PhotonNetwork.Disconnect();
    }
}