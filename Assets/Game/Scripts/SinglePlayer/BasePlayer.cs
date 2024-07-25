using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour, IAnimateable, IControlable
{
    #region INSPECTOR FIELDS

    [Header("HorizontalMovement")]
    [SerializeField] protected Joystick _joystick;
    [SerializeField] protected float _speed;
    [SerializeField] protected bool _faceRight;
    [SerializeField] protected bool _airMove;
    [Range(0, 1)]
    [SerializeField] protected float _airMoveModificator;

    [Header("Jump")]
    [SerializeField] protected float _jumpPower;
    [SerializeField] protected Transform _groundChecker;
    [SerializeField] protected float _groundCheckRadius;
    [SerializeField] protected LayerMask _whatIsGround;

    [Header("Crouch")]
    [SerializeField] protected BoxCollider2D _boxCrouchCollider2D;
    [SerializeField] protected Transform _ceilChecker;
    [Range(0, 1)]
    [SerializeField] protected float _crouchModificator;
    [SerializeField] protected bool _canStand;

    [Header("Animation")]
    [SerializeField] protected Animator _animator;

    [Header("Shoot")]
    [SerializeField] protected Transform _shootPointRight;
    [SerializeField] protected Transform _shootPointLeft;

    [Header("HP")]
    [SerializeField] protected int _hp;
    [SerializeField] protected BoxCollider2D _boxDeadCollider2D;

    [Header("Sound Effects")]
    [SerializeField] public AudioClip[] _audioClips;

    #endregion

    #region FIELDS

    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;
    protected AnimationController _animationController;
    protected BoxCollider2D[] _colliders;
    protected AudioSource _audioSource;
    protected SoundPlayerManager _soundPlayerManager;

    protected float _direction; // NEED TO INITIALIZATION (Joystick or Input) 
    protected float _speedModificator;
    protected bool _jump;
    protected bool _crouch;
    protected bool _isGrounded;
    protected bool _isOnEnemy;
    protected bool _pressShoot;
    protected bool _isDead;
    protected bool _play;

    #endregion

    #region ANIMATION PROPERTIES
    //IMPLEMENTATION IAnimateable

    public Animator Animator
    {
        get { return _animator; }
        protected set { _animator = value; }
    }
    public bool IsShoot { get; protected set; }
    public bool IsWalk { get; protected set; }
    public bool IsRun { get; protected set; }
    public bool IsJump { get; protected set; }
    public bool IsFall { get; protected set; }
    public bool IsCrouch { get; protected set; }
    public bool IsDead { get; protected set; }

    #endregion

    #region PROPERTIES

    public int Hp => _hp;
    public AudioSource AudioSource { get => _audioSource;}

    #endregion

    #region MOVEMENT METHODS
    //VIRTUAL methods to movement

    protected virtual void Move()
    {
        _rigidbody2D.velocity = new Vector2(x: _speed * _direction * _speedModificator, y: _rigidbody2D.velocity.y);
    }

    protected virtual void Jump()
    {
        if (_jump)
        {
            if (_isGrounded && _rigidbody2D.velocity.y == 0 || _isOnEnemy)
            {
                _rigidbody2D.AddForce(Vector2.up * _jumpPower);
            }
        }

        _jump = false;
    }

    protected virtual void Crouch()
    {
        if (_crouch || !_canStand)
        {
            if (_isGrounded && _rigidbody2D.velocity.y == 0 || _isOnEnemy)
            {
                _boxCrouchCollider2D.enabled = false;
            }
        }
        else
        {
            _boxCrouchCollider2D.enabled = true;
        }
    }

    protected virtual void SpeedModificator()
    {
        if (!_isGrounded && _airMove && _rigidbody2D.velocity.y != 0)
        {
            _speedModificator = _airMoveModificator;

        }
        else if (!_boxCrouchCollider2D.enabled)
        {
            _speedModificator = _crouchModificator;
        }
        else
        {
            _speedModificator = 1f;
        }
    }

    #endregion

    #region FIGHT METHODS
    // ABSTRACT methods for fighting

    public abstract void TakeDamage(int damage);
    protected abstract IEnumerator Shoot();
    protected abstract IEnumerator Dead();

    #endregion

    #region FACE DIRECTION
    //VIRTUAL methods to flip sprite personage

    protected virtual void SetDirection()
    {
        if (_faceRight && _direction < 0)
        {
            Flip();
        }
        else if (!_faceRight && _direction > 0)
        {
            Flip();
        }
    }

    protected virtual void Flip()
    {
        _faceRight = !_faceRight;

        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    #endregion

    #region EVENT SETTERS
    // Setters from UI buttons (IControlable implementation)

    public void SetJump()
    {
        _jump = true;
    }

    public void SetCrouch()
    {
        _crouch = true;
    }

    public void ResetCrouch()
    {
        _crouch = false;
    }

    public void SetShoot()
    {
        if (!_pressShoot)
        {
            StartCoroutine(Shoot());
        }
    }
    #endregion

    protected void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_groundChecker.position, _groundCheckRadius);
    }

    public virtual void PhysicsChecker()
    { 
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);
        _canStand = !Physics2D.OverlapBox(_ceilChecker.position, _boxCrouchCollider2D.size / 2, 0, _whatIsGround);
    }

    public virtual void AnimationSetter()
    {
        _animationController.AnimationStateMachine();

        IsShoot = _pressShoot;
        IsJump = !_isGrounded && _rigidbody2D.velocity.y > 0;
        IsFall = !_isGrounded && _rigidbody2D.velocity.y < 0;
        IsCrouch = !_boxCrouchCollider2D.enabled;
        IsWalk = _isGrounded && _rigidbody2D.velocity.x != 0 && _rigidbody2D.velocity.x < 1.5 && _rigidbody2D.velocity.x > -1.5 && _rigidbody2D.velocity.y == 0;
        IsRun = _isGrounded && _rigidbody2D.velocity.y == 0 && _rigidbody2D.velocity.x > 1.5 || _rigidbody2D.velocity.x < -1.5;
        IsDead = _isDead;

    }

    public virtual IEnumerator SoundSetter()
    {
        if (!_play)
        {
            _play = true;
            _soundPlayerManager.SoundStateMachine();

            yield return new WaitForSeconds(0.5f);

            _play = false;
        }
    }
}
