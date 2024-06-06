using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnOptionsButton()
    {
        optionsPanel.SetActive(true);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
    
}
