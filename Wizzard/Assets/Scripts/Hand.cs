using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public FireWand fireWand;
    public LightningWand lightningWand;
    
    
    public
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleScripts();
            Debug.Log("clicked");
        }
    }
    void ToggleScripts()
    {
        if (fireWand.enabled == true)
        {
            lightningWand.enabled = true;
            fireWand.enabled = false;
        }
        else if (lightningWand.enabled == true)
        {
            lightningWand.enabled = false;
            fireWand.enabled = true;
        }
    }
    
}
