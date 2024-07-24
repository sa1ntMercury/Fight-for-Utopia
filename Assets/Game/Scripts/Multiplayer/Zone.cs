using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Zone : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _zoneFrequency;
    [SerializeField] private float _damageFrequency;

    private List<BasePlayer> _players;

    private Transform _zone;
    private bool _increase;
    private bool _damaging;



    void Start()
    {
        _zone = GetComponent<Transform>();
        _players = new List<BasePlayer>(MultiplayerSettings._maxPlayers);

    }

    void FixedUpdate()
    {

        if (!_increase && _zone.localScale.y > 1f)
        {
            StartCoroutine(Decrease());
        }

        if (!_damaging && _players.Count > 0)
        {
            StartCoroutine(Damage());
        }
    }


    private IEnumerator Decrease()
    {
        _increase = true;
        _zone.localScale -= new Vector3(1f, 0.5f, 0);
        yield return new WaitForSeconds(_zoneFrequency);
        _increase = false;
    }

    private IEnumerator Damage()
    {
        Debug.Log($"COUNT {_players.Count}");
        _damaging = true;

        _players.Remove(null);

        foreach (var player in _players)
        {
            if (player != null)
            {
                player.TakeDamage(_damage);
            }
        }

        yield return new WaitForSeconds(_damageFrequency);
        _damaging = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BasePlayer player))
        {
            _players.Add(player);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BasePlayer player))
        {
            _players.Remove(player);
        }
    }

    
}