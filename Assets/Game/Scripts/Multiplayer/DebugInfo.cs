using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text; // Text for debug
    private const int MAXLENGHT = 250;
    public static DebugInfo Instance;

    private void Awake() => Instance = this; // Save component as singleton

    private void FixedUpdate()
    {
        if(_text.text.Length > MAXLENGHT)
        {
            ClearDebug();
            DebugInfo.Instance.DebugMessage("Connected with server...");
        }
    }

    // Show text on screen
    public void DebugMessage(string message)
    {
        _text.text += $"\n {message}";
    }

    public void ClearDebug()
    {
        _text.text = "";
    }
}
