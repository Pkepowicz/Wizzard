using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SmallMenuManager : MonoBehaviour
{
    public void ReloadScene()
    {
        // Get the current active scene's name and reload it.
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
