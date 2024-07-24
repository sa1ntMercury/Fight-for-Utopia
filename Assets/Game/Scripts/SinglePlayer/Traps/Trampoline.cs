using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trampoline : MonoBehaviour
{
    Animator _animator;
    AreaEffector2D _effector;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _effector = GetComponent<AreaEffector2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BasePlayer movement))
        {
            _animator.SetBool("Trampoline", true);
            _effector.forceMagnitude = 240;
            StartCoroutine(AnimationOff());
        }

    }

    private IEnumerator AnimationOff()
    {
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("Trampoline", false);
        _effector.forceMagnitude = default;
    }
}
