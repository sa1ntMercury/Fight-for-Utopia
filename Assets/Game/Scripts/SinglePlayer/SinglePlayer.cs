using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(Rigidbody2D))]

public class SinglePlayer : BasePlayer
{
    #region Additional Inspector Fields

    [Header("Bullet")]
    [SerializeField] protected Bullet _bullet;

    [Range(0, 10)]
    [SerializeField] protected float _shootingRangeModificator;

    #endregion

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animationController = new AnimationController(this);
    }

    private void Update()
    {

        #region INPUT SYSTEM
        // FOR USE WITH INPUT SYSTEM

        //_direction = Input.GetAxisRaw("Horizontal"); //-1 - A (<) : 1 - D (>) : 0 noting press
        //if (Input.GetButtonDown("Jump"))
        //    _jump = true;

        #endregion

        _direction = _joystick.Horizontal;
    }

    private void FixedUpdate()
    {
        PhysicsChecker();

        SetDirection();

        Crouch();

        Jump();

        AnimationSetter();

        SpeedModificator();

        Move();
    }    

    #region Fight Methods
    // Methods for fighting

    protected override IEnumerator Shoot() //Added to event method SetShoot
    {
        _pressShoot = true;

        yield return new WaitForSeconds(0.125f);

        Bullet bullet = Instantiate(_bullet, transform);

        if (_faceRight)
        {

            bullet.transform.position = _shootPointRight.position;
            bullet.MoveTo(_shootPointRight.position + Vector3.right * _shootingRangeModificator);
        }
        else
        {
            bullet.transform.position = _shootPointLeft.position;
            bullet.MoveTo(_shootPointRight.position + Vector3.left * _shootingRangeModificator);
        }
        yield return new WaitForSeconds(0.125f);
        _pressShoot = false;
    }

    public override void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            StartCoroutine(Dead());
        }
    }

    protected override IEnumerator Dead()
    {
        _rigidbody2D.Sleep();
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
    #endregion

}
