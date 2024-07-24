using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    private Tweener _moveTweener;

    public void MoveTo(Vector2 position)
    {
        float time = Mathf.Abs(position.x - transform.position.x) / _speed;
        _moveTweener = transform.DOMoveX(position.x, time).SetEase(Ease.Linear);
        _moveTweener.OnComplete(() => Destroy(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BasePlayer player))
            player.TakeDamage(_damage);
        if (collision.TryGetComponent(out BaseEnemy enemy))
            enemy.TakeDamage(_damage);
        if (collision.TryGetComponent(out Obstacles obstacles))
            obstacles.TakeDamage(_damage);
        Destroy(gameObject);
    }

    public static implicit operator Bullet(GameObject v)
    {
        return Bullet.Instantiate(new Bullet(), v.transform);
    }
}
