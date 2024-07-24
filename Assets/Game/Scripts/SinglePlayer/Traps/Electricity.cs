using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    [SerializeField] private int _damage;

    private SpriteRenderer _spriteRenderer;
    private Color _color;
    private Color _yellowColor;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
        _yellowColor = new Color(225, 225, 0, 165);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BasePlayer player))
        {
            player.TakeDamage(_damage);
        }
        StartCoroutine(SwitchColor());
    }

    private IEnumerator SwitchColor()
    {
        _spriteRenderer.color = _yellowColor;
        yield return new WaitForSeconds(1f);
        _spriteRenderer.color = _color;

    }

}
