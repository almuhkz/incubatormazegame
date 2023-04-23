using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayBlitz()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayClassic()
    {
        SceneManager.LoadScene(5);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(3);
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenLeaderboard()
    {
        SceneManager.LoadScene(4);
    }
}
