using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    public float speed = 0.8f;
    public float maxVelocity = 1.6f;
    public Vector3 velocity;

    // references TODO: Change it to private
    public Transform textureManager;
    public Camera mainCam;

    private void FixedUpdate()
    {
        // getting movement input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // looking at mouse position, TODO: look for better way to do this
        // not sure how it works but it works
        Vector3 toTarget = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        textureManager.transform.up = new Vector3(toTarget.x, toTarget.y, 0);

        UpdateMotor(new Vector3(x, y, 0).normalized);
    }

    // Maybe change it to simpler version without sliding?
    private void UpdateMotor(Vector3 moveDelta)
    {
        // acceleration
        if (Mathf.Abs(velocity.x) <= maxVelocity)
        {
            velocity.x += moveDelta.x * speed;
            velocity.x = Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity);
        }
        if (Mathf.Abs(velocity.y) <= maxVelocity)
        {
            velocity.y += moveDelta.y * speed;
            velocity.y = Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity);
        }
        // slowing
        if (velocity != Vector3.zero)
        {
            if (moveDelta.x == 0)
                velocity.x = Vector3.MoveTowards(velocity, Vector3.zero, speed).x;
            if (moveDelta.y == 0)
                velocity.y = Vector3.MoveTowards(velocity, Vector3.zero, speed).y;
        }
        // move
        transform.Translate(velocity * Time.deltaTime);
    }

    protected override void Death()
    {
        Time.timeScale = 0;
    }
}
