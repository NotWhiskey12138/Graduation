using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    private bool isGamePaused = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("jjjjjjj");
            if (isGamePaused)
            {
                OnResumeGame();
            }
            else
            {
                OnPauseGame();
            }
        }
    }

    public void OnPauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void OnResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    
    public void OnOptionsButton()
    {
        optionsPanel.SetActive(true);
    }

    public void OnExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
