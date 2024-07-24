using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public GameObject _scorePanel;
    public GameObject _losePanel;
    public GameObject _victoryPanel;

    public TMP_Text _myScoreText;
    public TMP_Text _enemyScoreText;

    private PhotonView _photonView;
    private byte _readyPlayers;

    private bool _moveNext;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_photonView.IsMine)
        {
            if (_readyPlayers >= 2)
            {
                SceneManager.LoadScene("MultiplayerGame");
            }
        }

        if (!_moveNext)
        {
            if (Score.MyPoints >= Score.MaxDefeats)
            {
                Debug.Log("Victory");

                StartCoroutine(VictoryProcess());
            }

            if (Score.EnemyPoints >= Score.MaxDefeats)
            {
                Debug.Log("Lose");

                StartCoroutine(LoseProcess());
            }

            if (!_moveNext)
            {
                StartCoroutine(FightScore());
            }
        }
    }

    private IEnumerator FightScore()
    {
        _moveNext = true;

        _myScoreText.text = $"{Score.MyPoints}";
        _enemyScoreText.text = $"{Score.EnemyPoints}";

        yield return new WaitForSeconds(5f);
        _photonView.RPC("ReadyForRound", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ReadyForRound()
    {
        _readyPlayers += 1;
    }

    public IEnumerator LoseProcess()
    {
        _moveNext = true;

        _scorePanel.SetActive(false);
        _losePanel.SetActive(true);

        yield return new WaitForSeconds(5f);

        Score.Nullify();

        ExitMultiplayer();
    }

  
    public IEnumerator VictoryProcess()
    {
        _moveNext = true;

        _scorePanel.SetActive(false);
        _victoryPanel.SetActive(true);

        yield return new WaitForSeconds(5f);

        Score.Nullify();

        ExitMultiplayer();
    }

    public void ExitMultiplayer()
    {
        SceneManager.LoadScene("MainMenu");
        PhotonNetwork.Disconnect();
    }
}
