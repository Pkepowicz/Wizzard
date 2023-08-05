using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject credits; 
    
    
    

    
    public void GoToNextScene()
    {
        SceneManager.LoadScene("Arena");
    }

 

      public void ToggleObject()
    {
        credits.SetActive(true);
        
    }

    
    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
