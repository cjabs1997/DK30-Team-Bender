using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _controls;
    [SerializeField] private GameObject _credits;

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowControls()
    {
        _controls.SetActive(true);
    }

    public void HideControls()
    {
        _controls.SetActive(false);
    }

    public void ShowCredits()
    {
        _credits.SetActive(true);
    }

    public void HideCredits()
    {
        _credits.SetActive(false);
    }
}
