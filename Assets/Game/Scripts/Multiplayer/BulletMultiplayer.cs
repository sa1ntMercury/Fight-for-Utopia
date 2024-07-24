using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;

public class BulletMultiplayer : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private float _destroyTime;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
   

    private bool _isShootLeft;
    private SpriteRenderer _spriteRenderer;  

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(DestroyBullet());
        if (_isShootLeft)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    private void Update()
    {
        if (!_isShootLeft)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_destroyTime);
        this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BasePlayer player))
        {
            player.TakeDamage(_damage);
            Destroy();
        }
        
    }

    [PunRPC]
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    [PunRPC]
    public void ChangeDirection()
    {
        _isShootLeft = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(_spriteRenderer.flipX);
        }
        else
        {
            // Network player, receive data
            this._spriteRenderer.flipX = (bool)stream.ReceiveNext();
        }
    }
}
