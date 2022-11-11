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
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = Vector2.Lerp(player.transform.position, mouseWorldPosition, 0.5f);
        Vector2 pos = Vector2.Lerp(transform.position, desiredPosition, 0.1f);
        transform.position = new Vector3(pos.x, pos.y, -10);
        
    }
}
