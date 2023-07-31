using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool isFireWandEnebled = true;
    private bool isStormWandEnabled = true;
    
    public GameObject stormWand;
    public GameObject fireWand;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleScripts();
        }
    }
    void ToggleScripts()
    {
        // Toggle Script A
        isFireWandEnebled = !isFireWandEnebled;
        fireWand.SetActive(isFireWandEnebled);

        // Toggle Script B
        isStormWandEnabled = !isStormWandEnabled;
        stormWand.SetActive(isStormWandEnabled);
    }
}
