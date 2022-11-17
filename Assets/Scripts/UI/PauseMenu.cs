using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private UnityEngine.UI.Button _resumeButton;
    [SerializeField] private TMPro.TextMeshProUGUI _topText;

    public void OpenDeathMenu()
    {
        _topText.text = "GAME OVER";
        _resumeButton.interactable = false;
        _pauseMenu.SetActive(true);
    }

    public void OpenWinMenu()
    {
        _topText.text = "YOU WIN";
        Time.timeScale = 0;
        _resumeButton.interactable = false;
        _pauseMenu.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        _topText.text = "PAUSED";
        Time.timeScale = 0;
        _resumeButton.interactable = true;
        _pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
