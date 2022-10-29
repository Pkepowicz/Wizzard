using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    public float speed = 0.8f;
    public float maxVelocity = 1.6f;
    public Vector3 velocity;
    protected RaycastHit2D hit;

    public Transform textureManager;
    public Camera mainCam;
    public Rigidbody2D rb;

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

    // Maybe change it to simpler version without sliding?
    private void UpdateMotor(Vector2 moveDelta)
    {
        rb.MovePosition(rb.position + moveDelta * speed * Time.deltaTime);
    }

    protected override void Death()
    {
        Time.timeScale = 0;
    }
}
