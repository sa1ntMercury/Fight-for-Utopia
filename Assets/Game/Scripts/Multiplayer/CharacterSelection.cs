using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class CharacterSelection : MonoBehaviourPunCallbacks
{

    [SerializeField] private Sprite[] _avatars;
    [SerializeField] private GameObject _avatarPanel;
    [SerializeField] private TMP_Text _name;

    private byte _readyPlayers;
    private Image _avatarCharacter;
    private int _avatarNumber;

    private PhotonView _photonView;


    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _avatarCharacter = _avatarPanel.GetComponent<Image>();
        _avatarCharacter.sprite = _avatars[_avatarNumber];
        ChangeAvatar();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_photonView.IsMine)
        {
            if (_readyPlayers == MultiplayerSettings._maxPlayers)
            {
                SceneManager.LoadScene("MultiplayerGame");
            }
        }
    }

    private void ChangeAvatar()
    {
        _avatarCharacter.sprite = _avatars[_avatarNumber];
        _name.text = _avatarCharacter.sprite.name;
        MultiplayerSettings._prefabName = _name.text;
    }

    public void OnLeft()
    {
        if (_avatarNumber == 0)
        {
            _avatarNumber = _avatars.Length - 1;
        }
        else
        {
            _avatarNumber -= 1;
        }
        ChangeAvatar();
    }

    public void OnRight()
    {
        if (_avatarNumber == _avatars.Length - 1)
        {
            _avatarNumber = 0;
        }
        else
        {
            _avatarNumber += 1;
        }
        ChangeAvatar();
    }

    public void OnFight()
    {
        _photonView.RPC("Fight", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void Fight()
    {
        _readyPlayers += 1;
    }

    public void Exit()
    {
        _readyPlayers -= 1;
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

        SceneManager.LoadScene("MainMenu");
    }
}

public static class MultiplayerSettings
{
    public static string _prefabName;
    public static int _maxPlayers;

}