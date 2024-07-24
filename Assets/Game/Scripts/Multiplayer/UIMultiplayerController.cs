using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMultiplayerController : MonoBehaviourPunCallbacks
{
    public MultiplayerPlayer _player;
    public GameObject _canvas;
    public GameObject _enemyCanvas;
    public Image[] _redSkullIcons;
    public Image[] _greenSkullIcons;

    private PhotonView _photonView;
    private bool _finish;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();

        if (!_photonView.IsMine)
        {
            _canvas.SetActive(false);
            _enemyCanvas.SetActive(true);
        }

        SkullSetter();
        
    }

    private void FixedUpdate()
    {

        if (!_finish && _player.Hp <= 0 && _photonView.IsMine)
        {
            Score.UpEnemyPoints();

            _photonView.RPC("UpPointsToEnemy", RpcTarget.Others);

            _photonView.RPC("NextRoundProcess", RpcTarget.All);

        }

    }

    [PunRPC]
    public IEnumerator NextRoundProcess()
    {
        _finish = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("ScoreScene");
    }

    [PunRPC]
    public void UpPointsToEnemy()
    {
        Score.UpMyPoints();
    }

    private void SkullSetter()
    {
        for (int i = 0; i < Score.MyPoints; i++)
        {
            _greenSkullIcons[i].enabled = true;
        }

        for (int i = 0; i < Score.EnemyPoints; i++)
        {
            _redSkullIcons[i].enabled = true;
        }
    }

}