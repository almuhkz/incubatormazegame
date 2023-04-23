using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject PausePanel;
    public void pauseGame()
    {
        PausePanel.SetActive(true);
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState;
        if (currentGameState == GameState.Gameplay) {
            newGameState = GameState.Paused;
            ScoreManager.isPaused = true;
        } else {
            newGameState = GameState.Gameplay;
            PausePanel.SetActive(false);
            ScoreManager.isPaused = false;
        }
        GameStateManager.Instance.SetState(newGameState);

    }

    public void quitButton()
    {
        Application.Quit();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }
}
