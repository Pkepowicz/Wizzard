using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    public Vector3 velocity;

    public Transform textureManager;
    public Camera mainCam;

    
    // for testing new effects
    public GameObject regen;

    protected override void Update()
    {
        base.Update();
        
        // for testing new effects
        if (Input.GetButtonDown("Submit"))
        {
            Instantiate(regen, gameObject.transform);
        }
    }

    private void FixedUpdate()
    {
        // getting movement input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // looking at mouse position, TODO: look for better way to do this
        // not sure how it works but it works
        Vector3 toTarget = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        textureManager.transform.up = new Vector3(toTarget.x, toTarget.y, 0);

        UpdateMotor(new Vector2(x, y).normalized);
    }
    
}
