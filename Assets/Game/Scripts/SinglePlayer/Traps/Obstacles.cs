using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    private int _hp = 100;

    private SpriteRenderer _spriteRenderer;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void TakeDamage(int damage)
    {
        _hp -= damage;

        _spriteRenderer.color -= new Color(0, 0.1f, 0, 0.05f);

        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
