using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static GameObject currentMenu = null;
    public GameObject pauseMenu;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (currentMenu == null)
            {
                OpenPauseMenu();
                Time.timeScale = 0f;
                GameIsPaused = true;
            }
            else
            {
                Debug.Log("Closing Menu" + currentMenu.name);
                currentMenu.GetComponent<Menu>().CloseMenu();
                
            }
        }
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        currentMenu = pauseMenu;
    }

 
    
}
