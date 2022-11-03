using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    public Vector3 velocity;

    public Transform textureManager;
    public Camera mainCam;
    
    // For testing new effects
    public GameObject regen;

    private void FixedUpdate()
    {
        // Getting movement input on each separate axis
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // For testing new effects
        if (Input.GetButtonDown("Submit"))
        {
            Instantiate(regen, transform.position, Quaternion.identity, gameObject.transform);
        }

        // looking at mouse position, TODO: look for better way to do this
        // not sure how it works but it works
        Vector3 toTarget = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        textureManager.transform.up = new Vector3(toTarget.x, toTarget.y, 0);

        UpdateMotor(new Vector2(x, y).normalized);
    }
    
}
