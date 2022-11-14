using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    public GameObject settingsMenu;

    /*public void Resume()
    {
        MenuManager.currentMenu = null;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }*/
    
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        MenuManager.currentMenu = settingsMenu;
        gameObject.SetActive(false);
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public override void CloseMenu()
    {
        Time.timeScale = 1f;
        MenuManager.currentMenu = null;
        MenuManager.GameIsPaused = false;
        gameObject.SetActive(false);
    }
}
