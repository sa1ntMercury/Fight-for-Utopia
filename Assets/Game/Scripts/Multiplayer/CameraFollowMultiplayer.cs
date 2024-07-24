using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMultiplayer : MonoBehaviour
{
    [SerializeField] public Transform? _player;
    [SerializeField] private int _speed;

    private Vector3 _playerVector;

    void Update()
    {
        if(_player == null)
        {
            return;
        }
        else
        {
            _playerVector = _player.position;
            //_playerVector.z = -5;
            transform.position = Vector3.Lerp(transform.position, new Vector3(_playerVector.x + 0.6f, _playerVector.y + 0.8f, _playerVector.z - 5), _speed * Time.deltaTime);
        }
    }
}
