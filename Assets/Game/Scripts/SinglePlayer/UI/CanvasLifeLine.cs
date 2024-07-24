using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasLifeLine : MonoBehaviour
{
    [SerializeField] BasePlayer _player;

    Slider _slider;


    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        _slider.value = _player.Hp;

    }

}
