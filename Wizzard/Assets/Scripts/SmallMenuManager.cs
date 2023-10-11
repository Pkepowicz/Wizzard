using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SmallMenuManager : MonoBehaviour
{
    public ScoreManager sm;
    public void ReloadScene()
    {
        // Get the current active scene's name and reload it.
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Debug.Log(ScoreManager.score);
        ScoreManager.score = 0;
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
