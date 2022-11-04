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

    [Header("Dash settings")] 
    private bool canDash = true;
    private bool isDashing = false;
    public float dashingPower = 20f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    public GameObject startDashParticle;
    public GameObject endDashParticle;

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        // if the player is already dashing, he can't do anything
        if (isDashing)
        {
            return;
        }
        // Getting movement input on each separate axis
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // For testing new effects
        /*if (Input.GetButtonDown("Submit"))
        {
            Instantiate(regen, transform.position, Quaternion.identity, gameObject.transform);
        }*/

        

        // looking at mouse position, TODO: look for better way to do this
        // not sure how it works but it works
        Vector3 toTarget = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        textureManager.transform.up = new Vector3(toTarget.x, toTarget.y, 0);

        UpdateMotor(new Vector2(x, y).normalized);
    }

    private IEnumerator Dash()
    {
        Vector3 targetPosition = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        canDash = false;
        isDashing = true;
        HandleParticles(startDashParticle);
        
        rb.velocity = targetPosition * dashingPower;
        yield return new WaitForSeconds(dashingTime);
        HandleParticles(endDashParticle);
        rb.velocity = Vector3.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    
}
