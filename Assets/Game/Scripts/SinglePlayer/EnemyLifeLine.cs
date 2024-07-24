using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeLine : MonoBehaviour
{
    [SerializeField] BaseEnemy _enemy;
    [SerializeField] GameObject _position;
    Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        _slider.transform.position = new Vector2(_position.transform.position.x, _position.transform.position.y);

        _slider.value = _enemy.Hp;
    }

}
