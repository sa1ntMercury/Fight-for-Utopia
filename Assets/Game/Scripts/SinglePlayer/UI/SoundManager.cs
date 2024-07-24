using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.value = Settings._soundLevel;
    }

    private void Update()
    {
        Settings._soundLevel = _slider.value;
    }
}
