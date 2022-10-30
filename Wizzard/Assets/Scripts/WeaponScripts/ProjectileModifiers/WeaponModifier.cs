using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModifier : MonoBehaviour
{
    private GameObject currentBullet;

    public void Execute(List<string> modifiers)
    {
        foreach(string modifier in modifiers)
        {
            Invoke(modifier, 0);
        }
    }
}
