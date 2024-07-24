using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public abstract class BaseEnemy : MonoBehaviour,  IAnimateable
{
    #region Inspector Fields

    [SerializeField] protected int _hp;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Transform _shootPoint;
    [SerializeField] protected Bullet _bullet;
    [Range(0, 10)]
    [SerializeField] protected float _shootingRangeModificator;

    #endregion

    #region Fields

    protected SpriteRenderer _spriteRenderer;
    protected AnimationController _animationController;
    protected bool _reload;
    protected bool _isShoot;
    protected bool _isDead;

    public int Hp => _hp;

    #endregion

    #region ANIMATION PROPERTIES
    //IMPLEMENTATION IAnimateable

    public Animator Animator
    {
        get { return _animator; }
        protected set { _animator = value; }
    }

    public bool IsShoot
    {
        get { return _isShoot; }
        protected set { _isShoot = value; }
    }

    public bool IsDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }
    public bool IsWalk { get; protected set; }
    public bool IsRun { get; protected set; }
    public bool IsJump { get; protected set; }
    public bool IsFall { get; protected set; }
    public bool IsCrouch { get; protected set; }

    #endregion


    protected abstract IEnumerator Shoot();

    public abstract void TakeDamage(int damage);

    protected abstract IEnumerator Dead();

    public void AnimationSetter()
    {
        _animationController.AnimationStateMachine();
    }
}