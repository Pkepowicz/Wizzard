using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject previousMenu = null;
    
    public virtual void CloseMenu()
    {
        if (previousMenu != null)
        {
            previousMenu.SetActive(true);
            MenuManager.currentMenu = previousMenu;
            gameObject.SetActive(false);
        }
    }

}
