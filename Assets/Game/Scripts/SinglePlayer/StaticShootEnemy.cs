using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class StaticShootEnemy : BaseEnemy
{

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animationController = new AnimationController(this);
    }


    private void FixedUpdate()
    {
        Animator = _animator;

        if (!_reload)
        {
            StartCoroutine(Shoot());
        }

        AnimationSetter();

    }

    protected override IEnumerator Shoot()
    {
        IsShoot = true;
        _reload = true;

        yield return new WaitForSeconds(1f);
        IsShoot = false;

        Bullet bullet = Instantiate(_bullet, transform);

        bullet.transform.position = _shootPoint.position;

        bullet.MoveTo(_shootPoint.position + Vector3.right * _shootingRangeModificator);

        yield return new WaitForSeconds(5f);

        _reload = false;
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
        _isDead = true;

        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
