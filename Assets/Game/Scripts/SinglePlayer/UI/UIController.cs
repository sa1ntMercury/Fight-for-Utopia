using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //[SerializeField] private SinglePlayer _player;
    //[SerializeField] private GameObject _mainPanel;
    //[SerializeField] private GameObject _deadPanel;

    //private void FixedUpdate()
    //{
    //    if (_player.Hp <= 0)
    //    {
    //        StartCoroutine(Dead());
    //    }
    //}

    //public IEnumerator Dead()
    //{
    //    yield return new WaitForSeconds(2f);
    //    _mainPanel.SetActive(false);
    //    _deadPanel.SetActive(true);
    //}

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
