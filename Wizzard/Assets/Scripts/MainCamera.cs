using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Useless for now
    public Transform player;

    private void Update()
    {
        transform.position = player.transform.position;
    }
}
