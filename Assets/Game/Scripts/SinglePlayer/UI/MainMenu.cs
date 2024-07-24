using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SpriteRenderer _backGround;
    [SerializeField] public Sprite[] _backImages;

    private void Awake()
    { 
        _backGround.sprite = _backImages[Random.Range(0, _backImages.Length)];
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackGroundDown()
    {
        _backGround.color -= new Color(0, 0, 0, 0.5f);
    }

    public void BackGroundUp()
    {
        _backGround.color += new Color(0, 0, 0, 0.5f);
    }
}
