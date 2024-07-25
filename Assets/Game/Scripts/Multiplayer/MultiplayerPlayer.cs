

using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(Rigidbody2D))]

public class MultiplayerPlayer : BasePlayer, IPunObservable
{
    #region Additional Inspector Fields

    [Header("Bullet")]
    [SerializeField] protected BulletMultiplayer _bullet;

    [Range(0, 1)]
    [SerializeField] protected float _shootFrequency;

    #endregion

    #region Fields & Props

    private float _shootDelay = 0.125f;
    private PhotonView _photonView;
    public bool IsPhotonMine => _photonView.IsMine;



    #endregion

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animationController = new AnimationController(this);
        _photonView = GetComponent<PhotonView>();
        _colliders = GetComponents<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();

        if (_photonView.Owner.IsLocal)
        {
            Camera.main.GetComponent<CameraFollowMultiplayer>()._player = gameObject.transform;
        }
    }

    private void Start()
    {
        if (!IsPhotonMine)
        {
            gameObject.layer = 7;
        }
    }

    private void Update()
    {
        #region INPUT SYSTEM
        //FOR USE WITH INPUT SYSTEM

        //_direction = Input.GetAxisRaw("Horizontal"); //-1 - A (<) : 1 - D (>) : 0 noting press
        //if (Input.GetButtonDown("Jump"))
        //    _jump = true;

        #endregion

        _direction = _joystick.Horizontal;
    }

    private void FixedUpdate()
    {
        if (IsPhotonMine)
        {

            PhysicsChecker();

            SetDirection();

            Crouch();

            Jump();

            AnimationSetter();

            SpeedModificator();

            Move();
        }

        
    }

    #region Fight Methods
    // Methods for fighting

    protected override IEnumerator Shoot() //Added to event method SetShoot
    {
        _pressShoot = true;

        yield return new WaitForSeconds(_shootDelay);

        GameObject bullet;

        if (_faceRight)
        {
            bullet = PhotonNetwork.Instantiate(_bullet.name, _shootPointRight.position, Quaternion.identity);
        }
        else
        {
            bullet = PhotonNetwork.Instantiate(_bullet.name, _shootPointLeft.position, Quaternion.identity);
            bullet.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered);
        }

        yield return new WaitForSeconds(_shootFrequency - _shootDelay);
        _pressShoot = false;
    }

    public override void TakeDamage(int damage) //Calls in bullet script
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _isDead = true;
            StartCoroutine(Dead());
        }
    }

    protected override IEnumerator Dead() //Calls when hp <= 0
    {
        _isDead = true;

        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }

        _boxDeadCollider2D.enabled = true;

        yield return new WaitForSeconds(2f);
        _spriteRenderer.enabled = false;
        _isDead = false; 
    }
    #endregion

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
